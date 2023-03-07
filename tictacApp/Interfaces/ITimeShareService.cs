using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ITimeShareService
{
    Task<int> GetTimeSpendOnTags(DateTime startDate, DateTime endDate, int? tagToProcess, int[]? tagsToExclude);

    Task<Dictionary<PlannedActivity, int>> GetTimeSpentOnActivities(DateTime startDate, DateTime endDate, string typeOfActivity);
}