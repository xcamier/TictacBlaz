using Microsoft.EntityFrameworkCore;
using tictacApp.Interfaces;

namespace tictacApp.Helpers;

public class CharacteristicsServiceHelper
{
    public static async Task<bool> HasTwoLevelsOfParentsAsync(IGenericCRUDService crud, int? characteristicIdToCheck)
    {
        if (characteristicIdToCheck.HasValue)
        {
            using var context = crud.GetNewDBContext();
            if (context is not null)
            {
                var parentJustAbove = await context.Characteristics.SingleOrDefaultAsync(c => c.Id == characteristicIdToCheck.Value);
                
                return parentJustAbove is not null && parentJustAbove.ParentId.HasValue;
            }
        }
        
        return false;
    }
}