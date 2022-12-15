using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class CharacteristicsService: CommonService
{
    public CharacteristicsService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Characteristic[]> GetCharacteristicsAsync(int? parentId)
    {
        using var context = _dbFactory.CreateDbContext();
        
        if (parentId.HasValue)
        {
            return await context.Characteristics.
                                    Where(p => p.ParentCharacteristicId != null && p.ParentCharacteristicId == parentId).
                                    OrderBy(t => t.Description1).ToArrayAsync();
        }
        else
        {
            return await context.Characteristics.
                                    Where(p => p.ParentCharacteristicId == null).
                                    OrderBy(t => t.Description1).ToArrayAsync();
        }
    }


    public async Task<Characteristic?> FindCharacteristicFromIdAsync(TictacDBContext? dbContext, int characteristics)
    {    
        if (dbContext is not null && dbContext.Characteristics is not null)
        {
            return await dbContext.Characteristics.SingleOrDefaultAsync(c => c.Id == characteristics); 
        }

        return null;
    }

    public async Task<KeyValuePair<int, string?>[]> GetParentCharacteristics(int characteristicId)
    {
        List<KeyValuePair<int, string?>> parentCharacteristics = new List<KeyValuePair<int, string?>>();

        using var context = _dbFactory.CreateDbContext();

        int? characteristicIdToSearch = characteristicId;
        while (characteristicIdToSearch.HasValue)
        {
            //Characteristic? foundCharacteristic = await context.Characteristics.SingleOrDefaultAsync(p => p.Id == characteristicIdToSearch.Value);
            Characteristic? foundCharacteristic = await FindCharacteristicFromIdAsync(context, characteristicIdToSearch.Value);
            if (foundCharacteristic is not null)
            {
                KeyValuePair<int, string?> item = new KeyValuePair<int, string?>(foundCharacteristic.Id, foundCharacteristic.Description1);
                parentCharacteristics.Insert(0, item);

                characteristicIdToSearch = foundCharacteristic.ParentCharacteristicId;
            }
            else
            {
                characteristicIdToSearch = null;
            }
        }

        return parentCharacteristics.ToArray();
    }
    
    public async Task<bool> AddCharacteristicAsync(Characteristic? characteristicToAdd)
    {
        if (characteristicToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Characteristics?.Add(characteristicToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteCharacteristicAsync(Characteristic characteristicToDelete)
    {
        if (characteristicToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(characteristicToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }

    public async Task<bool> HasTwoLevelsOfParents(int? characteristicIdToCheck)
    {
        if (characteristicIdToCheck.HasValue)
        {
            using var context = _dbFactory.CreateDbContext();
            if (context is not null)
            {
                
                var parentJustAbove = await context.Characteristics.SingleOrDefaultAsync(c => c.Id == characteristicIdToCheck.Value);
                
                return parentJustAbove is not null && parentJustAbove.ParentCharacteristicId.HasValue;
            }
        }
        
        return false;
    }
}