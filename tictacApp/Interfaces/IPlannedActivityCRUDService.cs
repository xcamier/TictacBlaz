using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface IPlannedActivityCRUDService: IGenericCRUDServiceWithParents
{
    Task<T[]> GetAllAsync<T>(DateTime limitDate) where T: PlannedActivity;
    
    Task<T[]> GetAllNotClosedAsync<T>() where T: PlannedActivity;

    new Task<T?> FindFromIdAsync<T>(TictacDBContext? dbContext, int id) where T : class, IId, IComments;

    Task<IEnumerable<int>> GetIdOfPlannedActivitiesWithChildren<T>(int[] plannedActivitiesIds) where T : class, IId, IParent;
    
    Task<Comment?> FindCommentFromIdAsync(TictacDBContext? dbContext, int id);  

    Task<bool> DeleteCommentAsync(TictacDBContext? dbContext, Comment commentToDelete);

    void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity;

    Task<T[]> GetSubsetOfPlannedActivities<T>(int[] selectionOfActivities) where T: PlannedActivity;

    Task DeleteAllCommentsOfActivityAsync(TictacDBContext? dbContext, int plannedActivityId);

    Task DeleteAsync<T>(TictacDBContext? dbContext, T itemToDelete) where T: PlannedActivity;

    Task<bool> HasSubActivities<T>(int activityToCheck) where T: PlannedActivity;

    Task<T[]> GetPlannedActivitiesExpectedForQuarter<T>(DateTime startDate, DateTime endDate) where T: PlannedActivity;

    Task<T[]> GetPlannedActivitiesBehind<T>(DateTime today) where T: PlannedActivity;
}