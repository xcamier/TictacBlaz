using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class PlannedActivityCRUDService : GenericCRUDServiceWithParents, IPlannedActivityCRUDService
{
    private TimeLogsService _timelogsService;

    public PlannedActivityCRUDService(IDbContextFactory<TictacDBContext> dbFactory, TimeLogsService timelogsService): base(dbFactory)
    {
        _timelogsService = timelogsService;
    }

    public async Task<T[]> GetAllAsync<T>(DateTime limitDate) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().
                                Where(pa => pa.IsClosed == false && 
                                        pa.TargetDate != null && 
                                        pa.TargetDate <= limitDate &&
                                        pa.IsFinalized == false).
                                ToArrayAsync();
    }

    public async Task<T[]> GetAllNotClosedAsync<T>() where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().
                                Where(pa => pa.IsClosed == false).
                                ToArrayAsync();
    }

    async Task<T?> IPlannedActivityCRUDService.FindFromIdAsync<T>(TictacDBContext? dbContext, int id) 
                                                                     where T : class
    {    
        if (dbContext is not null && dbContext.Set<T>() is not null)
        {
            return await dbContext.Set<T>().
                Include(c => c.Comments).
                    ThenInclude(a => a.Attachments).
                SingleOrDefaultAsync(c => c.Id == id); 
        }

        return null;
    }

    public async Task<Comment?> FindCommentFromIdAsync(TictacDBContext? dbContext, int id)
    {
        if (dbContext is not null && dbContext.Comments is not null)
        {
            return await dbContext.Comments.FindAsync(id);
        }

        return null;
    }

    public async Task<IEnumerable<int>> GetIdOfPlannedActivitiesWithChildren<T>(int[] plannedActivitiesIds) where T : class, IId, IParent
    {
        using var context = _dbFactory.CreateDbContext();

        var foundIds = context.Set<T>().Where(pa => pa.ParentId != null && plannedActivitiesIds.Contains(pa.ParentId.Value)).
                                            Select(pa => pa.ParentId.Value).
                                            Distinct();

        return await foundIds.ToListAsync();
    }

    public async Task<bool> DeleteCommentAsync(TictacDBContext? dbContext,  Comment commentToDelete)
    {
        if (dbContext is not null && commentToDelete != null)
        {
            dbContext.Remove(commentToDelete);
            
            return await dbContext.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task DeleteAllCommentsOfActivityAsync(TictacDBContext? dbContext, int plannedActivityId)
    {
        if (dbContext is not null)
        {
            Comment[] allComments = await dbContext.Comments.Where(c => c.PlannedActivityId == plannedActivityId).ToArrayAsync();
            int[] allCommentsIds = allComments.Select(c => c.Id).ToArray();

            Attachment[] allAttachments = await dbContext.Attachments.Where(a => allCommentsIds.Contains(a.CommentId)).ToArrayAsync();

            dbContext.RemoveRange(allAttachments);
            dbContext.RemoveRange(allComments);
        }
    }

    public void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();
        
        IEnumerable<T> foundEntities = context.Set<T>().Where(t => t.ParentId == startId.Value);
        foreach (var entity in foundEntities)
        {
            int nbOfItems = values.Item1 + 1;
            int sum = values.Item2 + entity.CompletionPercent;
            values = new Tuple<int, int>(nbOfItems, sum);

            GetSumOfCompletionOfChildren<T>(entity.Id, ref values);
        }
    }

    public async Task<T[]> GetSubsetOfPlannedActivities<T>(int[] selectionOfActivities) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().
                            Where(o => o.IsClosed == false || selectionOfActivities.Contains(o.Id)).
                            ToArrayAsync();
    }

    public async Task DeleteAsync<T>(TictacDBContext? dbContext, T itemToDelete) where T : PlannedActivity
    {
        if (dbContext is not null)
        {
            await DeleteAllCommentsOfActivityAsync(dbContext, itemToDelete.Id);
            await _timelogsService.DetachFromActivity<T>(dbContext, itemToDelete.Id, typeof(T));

            dbContext.Set<T>().Remove(itemToDelete);
        }
    }

    public async Task<bool> HasSubActivities<T>(int activityToCheck) where T : PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().AnyAsync(a => a.ParentId == activityToCheck);
    }

    public async Task<T[]> GetPlannedActivitiesExpectedForQuarter<T>(DateTime startDate, DateTime endDate) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().Where(a => a.TargetDate != null && a.TargetDate >= startDate && a.TargetDate <= endDate).ToArrayAsync();
    }

    public async Task<T[]> GetPlannedActivitiesBehind<T>(DateTime today) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().Where(a => a.TargetDate < today || a.IsBehind).ToArrayAsync();
    }
}