using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class ActorsService: CommonService
{
    public ActorsService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Actor[]> GetActorsAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Actors.OrderBy(t => t.Name).ToArrayAsync();
    }

    public async Task<Actor?> FindActorFromIdAsync(TictacDBContext? dbContext, int actorId)
    {    
        if (dbContext is not null && dbContext.Actors is not null)
        {
            return await dbContext.Actors.SingleOrDefaultAsync(c => c.Id == actorId); 
        }

        return null;
    }

    public async Task<bool> AddActorAsync(Actor? actorToAdd)
    {
        if (actorToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Actors?.Add(actorToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteActorAsync(Actor actorToDelete)
    {
        if (actorToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(actorToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}