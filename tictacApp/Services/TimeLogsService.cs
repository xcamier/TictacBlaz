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
                        Where(t => t.Description != null && t.Description.ToUpper().Contains(searchStrig.ToUpper())).
                        OrderByDescending(t => t.StartDate).
                        Take(50).
                        ToArrayAsync();
    }

    public async Task<int> GetTimeSpentInWeekAsync(DateOnly currentDate)
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

    public async Task<bool> CheckIfPreviousTimelogIsAQuicklogAsync(DateTime currentDate)
    {
        DateTime dt = currentDate.Date;

        TimeLog? lastTimeLog = await GetLastTimelogAsync(dt);
        
        return IsTimelogAQuicklog(lastTimeLog);
    }

    public async Task<TimeLog?> GetPreviousTimelogIfItIsAQuicklogAsync(DateTime currentDate)
    {
        DateTime dt = currentDate.Date;

        TimeLog? lastTimeLog = await GetLastTimelogAsync(dt);

        bool isTimelogAQuicklog = IsTimelogAQuicklog(lastTimeLog);
        
        return isTimelogAQuicklog ? lastTimeLog : null;
    }

    private async Task<TimeLog?> GetLastTimelogAsync(DateTime dt)
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

    public async Task<int> CountLogsForObjective(int objectiveId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.TimeLogs.CountAsync(tl => tl.ObjectiveId == objectiveId);
    }

    public async Task<int> CountLogsForProject(int projectId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.TimeLogs.CountAsync(tl => tl.ProjectId == projectId);
    }

    public async Task DetachFromActivity<T>(TictacDBContext? dbContext, int activityIdToDetach, Type type)
    {
        TimeLog[] timelogsToUpdate;

        if (type == typeof(Objective))
        {
            timelogsToUpdate = await dbContext.TimeLogs.Where(tl => tl.ObjectiveId == activityIdToDetach).ToArrayAsync();
            foreach (TimeLog tl in timelogsToUpdate)
            {
                tl.ObjectiveId = null;
            }
        }
        else if (type == typeof(Project))
        {
            timelogsToUpdate = await dbContext.TimeLogs.Where(tl => tl.ProjectId == activityIdToDetach).ToArrayAsync();
            foreach (TimeLog tl in timelogsToUpdate)
            {
                tl.ProjectId = null;
            }
        }
        else
            throw new NotImplementedException($"Type should be either objective or Project. Type is {type.Name}");
    }

    public async Task<TimeLog[]> GetTimelogsOfObjectivesBetweenDates(int?[] objectivesIds, DateTime startDate, DateTime endDate)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.TimeLogs.Where(tl => tl.StartDate >= startDate && tl.StartDate <= endDate &&
                                                    objectivesIds.Contains(tl.ObjectiveId)).ToArrayAsync();
    }

    public async Task<TimeLog[]> GetTimelogsOfProjectsBetweenDates(int?[] objectivesIds, DateTime startDate, DateTime endDate)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.TimeLogs.Where(tl => tl.StartDate >= startDate && tl.StartDate <= endDate &&
                                                    objectivesIds.Contains(tl.ProjectId)).ToArrayAsync();
    }
}