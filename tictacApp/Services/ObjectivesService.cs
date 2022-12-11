using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class ObjectivesService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public ObjectivesService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Objective[]> GetObjectivesAsync(int? parentId)
    {
        using var context = _dbFactory.CreateDbContext();
        
        if (parentId.HasValue)
        {
            return await context.Objectives.
                                    Where(p => p.ParentObjective != null && p.ParentObjectiveId == parentId).
                                    OrderBy(t => t.Label).ToArrayAsync();
        }
        else
        {
            return await context.Objectives.
                                    Where(p => p.ParentObjective == null).
                                    OrderBy(t => t.Label).ToArrayAsync();
        }
    }

    public async Task<Objective?> FindObjectiveFromIdAsync(TictacDBContext? dbContext, int objectiveId)
    {    
        if (dbContext is not null && dbContext.Objectives is not null)
        {
            return await dbContext.Objectives.SingleOrDefaultAsync(c => c.Id == objectiveId); 
        }

        return null;
    }

    public async Task<KeyValuePair<int, string?>[]> GetParentObjectives(int objectiveId)
    {
        List<KeyValuePair<int, string?>> parentObjectives = new List<KeyValuePair<int, string?>>();

        using var context = _dbFactory.CreateDbContext();

        int? objectiveIdToSearch = objectiveId;
        while (objectiveIdToSearch.HasValue)
        {
            Objective? foundObjective = await context.Objectives.SingleOrDefaultAsync(p => p.Id == objectiveIdToSearch.Value);
            if (foundObjective is not null)
            {
                KeyValuePair<int, string?> item = new KeyValuePair<int, string?>(foundObjective.Id, foundObjective.Label);
                parentObjectives.Insert(0, item);

                objectiveIdToSearch = foundObjective.ParentObjectiveId;
            }
            else
            {
                objectiveIdToSearch = null;
            }
        }

        return parentObjectives.ToArray();
    }

    public async Task<bool> AddObjectiveAsync(Objective? objectiveToAdd)
    {
        if (objectiveToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Objectives?.Add(objectiveToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteObjectiveAsync(Objective objectiveToDelete)
    {
        if (objectiveToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(objectiveToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}