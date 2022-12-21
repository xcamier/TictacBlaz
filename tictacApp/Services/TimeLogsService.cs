using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class TimeLogsService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public TimeLogsService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<TimeLog[]> GetTimeLogsAsync(DateOnly currentDate)
    {
        using var context = _dbFactory.CreateDbContext();
        
        DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
        DateTime endDate = startDate.AddDays(1);

        return await context.TimeLogs.
                        Where(t => t.StartDate >= startDate && t.StartDate < endDate).
                        ToArrayAsync();
    }

    public async Task<int> GetTimeSpentInWeek(DateOnly currentDate)
    {
        using var context = _dbFactory.CreateDbContext();
        
        DateTime dt = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

        bool isSunday = dt.DayOfWeek == 0;
        var dayOfweek = isSunday == false ? (int)dt.DayOfWeek : 7;
        var startDate = dt.AddDays(((int)(dayOfweek) * -1) + 1);
        var endDate = startDate.AddDays(8);

        return await context.TimeLogs.
                        Where(t => t.StartDate >= startDate && t.StartDate < endDate).
                        SumAsync(s => s.TimeSpentInMin);
    }

    public async Task<TimeLog?> FindTimeLogFromIdAsync(TictacDBContext? dbContext, int timeLogId)
    {    
        if (dbContext is not null && dbContext.TimeLogs is not null)
        {
            return await dbContext.TimeLogs.
                                Include(t => t.Characteristics).
                                Include(t => t.Tags).
                                SingleOrDefaultAsync(c => c.Id == timeLogId); 
        }

        return null;
    }

    public async Task<bool> AddTimeLogAsync(TimeLog? timeLogToAdd)
    {
        if (timeLogToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
        
            context.TimeLogs?.Add(timeLogToAdd);
            //makes sure ef core doesn't try to add the characteristic to the collection of characteristics
            foreach (Data.Characteristic charToAttach in timeLogToAdd.Characteristics)
            {
                context.Entry(charToAttach).State = EntityState.Unchanged;
            }
            foreach (Tag tagToAttach in timeLogToAdd.Tags)
            {
                context.Entry(tagToAttach).State = EntityState.Unchanged;
            }

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }
}