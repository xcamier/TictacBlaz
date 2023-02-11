using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Helpers;

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

    public async Task<TimeLog[]> GetTimeLogsAsync(string searchStrig)
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.TimeLogs.
                        Where(t => t.Description != null && t.Description.Contains(searchStrig)).
                        OrderByDescending(t => t.StartDate).
                        Take(50).
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

    public async Task<bool> CheckIfPreviousTimelogIsAQuicklog(DateTime currentDate)
    {
        DateTime dt = currentDate.Date;

        TimeLog? lastTimeLog = await GetLastTimelog(dt);
        
        return IsTimelogAQuicklog(lastTimeLog);
    }

    public async Task<TimeLog?> GetPreviousTimelogIfItIsAQuicklog(DateTime currentDate)
    {
        DateTime dt = currentDate.Date;

        TimeLog? lastTimeLog = await GetLastTimelog(dt);

        bool isTimelogAQuicklog = IsTimelogAQuicklog(lastTimeLog);
        
        return isTimelogAQuicklog ? lastTimeLog : null;
    }

    private async Task<TimeLog?> GetLastTimelog(DateTime dt)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.TimeLogs.
                                Where(tl => tl.StartDate >= dt).
                                OrderByDescending(tl => tl.StartDate).
                                FirstOrDefaultAsync();
    }
    
    private bool IsTimelogAQuicklog(TimeLog? timelog)
    {
        return (timelog is not null && 
                    timelog.Description != null &&
                    timelog.Description.Contains(DefaultTexts.QuickTimelog));
    }
}