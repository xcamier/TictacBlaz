using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class PlannedActivityCRUDService : GenericCRUDServiceWithParents, IPlannedActivityCRUDService
{
    public PlannedActivityCRUDService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
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

    public async Task<bool> DeleteCommentAsync(TictacDBContext? dbContext,  Comment commentToDelete)
    {
        if (dbContext is not null && commentToDelete != null)
        {
            dbContext.Remove(commentToDelete);
            
            return await dbContext.SaveChangesAsync() > 0;
        }

        return false;
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
}