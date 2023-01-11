using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface IPlannedActivityCRUDService: IGenericCRUDServiceWithParents
{
    new Task<T?> FindFromIdAsync<T>(TictacDBContext? dbContext, int id) where T : class, IId, IComments;
    
    Task<Comment?> FindCommentFromIdAsync(TictacDBContext? dbContext, int id);  

    Task<bool> DeleteCommentAsync(TictacDBContext? dbContext, Comment commentToDelete);

    void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity;
}