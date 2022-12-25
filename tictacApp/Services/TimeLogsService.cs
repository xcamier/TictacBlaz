using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class TimeLogsService: TimelogObservation<TimeLog>
{
    public TimeLogsService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
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
}