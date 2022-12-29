using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class ObservationsService: TimelogObservation<Observation>
{
    public ObservationsService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<Observation[]> GetObservationsAsync(DateTime startDate, DateTime endDate, int actorId )
    {
        using var context = _dbFactory.CreateDbContext();
        
        return  await context.Observations.
                        Include(a => a.Actor).
                        Include(a => a.Tags).
                        Where(o => o.ObservationDate >= startDate && o.ObservationDate <= endDate &&
                                o.ActorId == actorId).ToArrayAsync();         
    }
}