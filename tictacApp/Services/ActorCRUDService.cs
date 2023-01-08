using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class ActorCRUDService : GenericCRUDService, IActorsCRUDService
{
    public ActorCRUDService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<Actor[]> GetAllAsync(bool showInactivesOnly)
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Actors.Where(a => a.IsInactive == showInactivesOnly).ToArrayAsync();
    }
}