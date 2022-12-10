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

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<TimeLog[]> GetTimeLogsAsync(DateOnly currentDate)
    {
        using var context = _dbFactory.CreateDbContext();
        
        DateTime dt = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

        bool isSunday = dt.DayOfWeek == 0;
        var dayOfweek = isSunday == false ? (int)dt.DayOfWeek : 7;
        var startDate = dt.AddDays(((int)(dayOfweek) * -1) + 1);
        var endDate = startDate.AddDays(8);

        return await context.TimeLogs.
                        Where(t => t.StartDate >= startDate && t.StartDate < endDate).
                        OrderByDescending(t => startDate).
                        ToArrayAsync();
    }

    public async Task<TimeLog?> FindTimeLogFromIdAsync(TictacDBContext? dbContext, int timeLogId)
    {    
        if (dbContext is not null && dbContext.TimeLogs is not null)
        {
            return await dbContext.TimeLogs.SingleOrDefaultAsync(c => c.Id == timeLogId); 
        }

        return null;
    }

    public async Task<bool> AddTimeLogAsync(TimeLog? timeLogToAdd)
    {
        if (timeLogToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.TimeLogs?.Add(timeLogToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteTimeLogAsync(TimeLog timeLogToDelete)
    {
        if (timeLogToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(timeLogToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}