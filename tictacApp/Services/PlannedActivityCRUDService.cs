using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class PlannedActivityCRUDService : GenericCRUDServiceWithParents, IPlannedActivityCRUDService
{
    public PlannedActivityCRUDService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public void GetSumOfCompletionOfChildren<T>(int? startId, ref Tuple<int, int> values) where T: PlannedActivity
    {
        using var context = _dbFactory.CreateDbContext();
        
        IEnumerable<T> foundEntities = context.Set<T>().Where(t => t.ParentId == startId.Value);
        foreach (var entity in foundEntities)
        {
            int nbOfItems = values.Item1 + 1;
            int sum = values.Item2 + entity.CompletionPercent;
            values = new Tuple<int, int>(nbOfItems, sum);

            GetSumOfCompletionOfChildren<T>(entity.Id, ref values);
        }
        
    }
}