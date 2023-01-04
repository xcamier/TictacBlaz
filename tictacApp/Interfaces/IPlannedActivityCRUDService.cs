using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface IPlannedActivityCRUDService
{
    void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity;
}