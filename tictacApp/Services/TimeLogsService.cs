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
        
        DateTime dt = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

		//var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        bool isSunday = dt.DayOfWeek == 0;
        var dayOfweek = isSunday == false ? (int)dt.DayOfWeek : 7;
        var startDate = dt.AddDays(((int)(dayOfweek) * -1) + 1);
        var endDate = startDate.AddDays(8);

        return await context.TimeLogs.
                        Where(t => t.StartDate >= startDate && t.StartDate < endDate).
                        OrderByDescending(t => startDate).
                        ToArrayAsync();
    }
}