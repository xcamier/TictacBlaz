using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface IPlannedActivityCRUDService: IGenericCRUDServiceWithParents
{
    Task<T[]> GetAllAsync<T>(DateTime limitDate) where T: PlannedActivity;

    new Task<T?> FindFromIdAsync<T>(TictacDBContext? dbContext, int id) where T : class, IId, IComments;

    Task<IEnumerable<int>> GetIdOfPlannedActivitiesWithChildren<T>(int[] plannedActivitiesIds) where T : class, IId, IParent;
    
    Task<Comment?> FindCommentFromIdAsync(TictacDBContext? dbContext, int id);  

    Task<bool> DeleteCommentAsync(TictacDBContext? dbContext, Comment commentToDelete);

    void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity;
}