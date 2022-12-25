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

    /*public async Task<Observation?> FindObservationFromIdAsync(TictacDBContext? dbContext, int observationId)
    {    
        if (dbContext is not null && dbContext.Observations is not null)
        {
            return await dbContext.Observations.
                                Include(t => t.Characteristics).
                                Include(t => t.Tags).
                                SingleOrDefaultAsync(c => c.Id == observationId); 
        }

        return null;
    }*/


}