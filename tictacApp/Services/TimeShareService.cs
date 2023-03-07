using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class TimeShareService : ITimeShareService
{
    private IDbContextFactory<TictacDBContext> _dbFactory;
    private IPlannedActivityCRUDService _plannedActivityCRUDService;

    public TimeShareService(IDbContextFactory<TictacDBContext> dbFactory, IPlannedActivityCRUDService plannedActivityCRUDService)
    {
        _dbFactory = dbFactory;
        _plannedActivityCRUDService = plannedActivityCRUDService;
    }

    public async Task<int> GetTimeSpendOnTags(DateTime startDate, DateTime endDate, 
                                                int? tagToProcess, int[]? tagsToExclude)
    {
        using var context = _dbFactory.CreateDbContext();
        
        //Gets all the timelogs for the tag between the dates
        //Forced to do it in several steps because the sqlite provider doesn't support the complex query
        IEnumerable<TimeLog>? logs = await context.TimeLogs.
                                                Include(l => l.Tags).
                                                Where(l => l.StartDate >= startDate && l.StartDate <= endDate).ToListAsync();

        //Filters on a tag if defined
        if (tagToProcess is not null)
        {
            //Find the tags entities
            Tag? foundTagToProcess = await context.Tags.FindAsync(tagToProcess);

            if (foundTagToProcess is null)
            {
                throw new KeyNotFoundException("Unable to find the tag that needs to be processed.");
            }

            logs = logs.Where(l => l.Tags != null && l.Tags.Contains(foundTagToProcess));
        }
        
        //Exclude some tags if defined
        if (tagsToExclude is not null)
        {
            IEnumerable<Tag> foundTagsToExclude = await context.Tags.Where(t => tagsToExclude.Contains(t.Id)).ToListAsync();

            if (foundTagsToExclude is null || foundTagsToExclude.Count() != tagsToExclude.Count())
            {
                throw new KeyNotFoundException("Unable to find the tags that needs to be excluded.");         
            }

            logs = logs.Where(t => t.Tags != null && t.Tags.All(tag => !tagsToExclude.Contains(tag.Id))); 
        }
        
        //Returns the sum of logs in minutes
        return logs.Sum(s => s.TimeSpentInMin);
    }

    public async Task<Dictionary<PlannedActivity, int>> GetTimeSpentOnActivities(DateTime startDate, DateTime endDate, 
                                                                                    string typeOfActivity)
    {
        if (typeOfActivity != "Objectives" && typeOfActivity != "Projects")
        {
            throw new NotImplementedException("Type of activity unknown. Retrieval of the time spent not implemented.");
        }

        Dictionary<PlannedActivity, int> timePerActivityDict = new Dictionary<PlannedActivity, int>();

        using var context = _dbFactory.CreateDbContext();

        //Isolates the logs corresponding to the criteria
        //IEnumerable<TimeLog>? allLogs = await ExtractLogs(startDate, endDate, typeOfActivity, context);
        Dictionary<int, int> sumPerActivity = await ExtractLogs(startDate, endDate, typeOfActivity, context);

        //Depending on whether we are looking for objectives or projects, 
        //make the lookup because we want to display the cumulative time
        //even for those for which there was no log, except for closed 
        //objectives that have no log for the period passed as parameter
        if (typeOfActivity == "Objectives")
        {
            timePerActivityDict = await AssociateLogsWithActivity<Objective>(sumPerActivity);
        }
        else if (typeOfActivity == "Projects")
        {
            timePerActivityDict = await AssociateLogsWithActivity<Project>(sumPerActivity);
        }

        return timePerActivityDict;
    }

    private async Task<Dictionary<PlannedActivity, int>> AssociateLogsWithActivity<T>(Dictionary<int, int> sumPerActivity) where T: PlannedActivity
    {
        int[] idsOfTheActivities = sumPerActivity.Select(spl => spl.Key).ToArray();

        //Query that makes sure we will display a value for all the activities (projects or objectives
        //depending on T parameter), and not only those for which there is at least one time log
        T[] relevantActivities = await _plannedActivityCRUDService.
                                                            GetSubsetOfPlannedActivities<T>(idsOfTheActivities);

        //Attaches the sum with the all the relevant activities.
        //If no sum is found => 0 is the value
        Dictionary<PlannedActivity, int> timePerActivityDict = new Dictionary<PlannedActivity, int>();
        foreach (T activity in relevantActivities)
        {
            int sum = sumPerActivity.ContainsKey(activity.Id) ? sumPerActivity[activity.Id] : 0; 
            timePerActivityDict.Add(activity, sum);
        }

        return timePerActivityDict;
    }

    private static async Task<Dictionary<int, int>> ExtractLogs(DateTime startDate, DateTime endDate, 
                                                                string typeOfActivity, TictacDBContext context)
    {
        dynamic sumPerActivityAnonymous;
        if (typeOfActivity == "Objectives")
        {
            sumPerActivityAnonymous = await context.TimeLogs.
                                                Where(l => l.StartDate >= startDate && 
                                                            l.StartDate <= endDate && 
                                                            l.ObjectiveId != null).
                                                GroupBy(gl => gl.ObjectiveId).
                                                Select(gl => new { ActivityId = gl.Key, Total = gl.Sum(gls => gls.TimeSpentInMin) }).
                                                ToArrayAsync();
        }
        else if (typeOfActivity == "Projects")
        {
            sumPerActivityAnonymous = await context.TimeLogs.
                                                Where(l => l.StartDate >= startDate && 
                                                            l.StartDate <= endDate && 
                                                            l.ProjectId != null).
                                                GroupBy(gl => gl.ProjectId).
                                                Select(gl => new { ActivityId = gl.Key, Total = gl.Sum(gls => gls.TimeSpentInMin) }).
                                                ToArrayAsync();
        }
        else 
            throw new NotImplementedException("Cannot extract the logs: type of activity unknown");


        //Presents the results as a dictionary for easier consolidation with the activities later
        Dictionary<int, int> sumPerActivity = new Dictionary<int, int>();
        foreach (var aSum in sumPerActivityAnonymous)
        {
            sumPerActivity.Add(aSum.ActivityId, aSum.Total);
        }

        return sumPerActivity;
    }
}