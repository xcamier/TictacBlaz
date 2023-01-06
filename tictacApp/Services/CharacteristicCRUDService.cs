using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class CharacteristicCRUDService : GenericCRUDServiceWithParents, ICharacteristicCRUDService
{
    public CharacteristicCRUDService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<Characteristic[]> GetAllAsync(int? parentId, bool displayGradeAndGroup)
    {
        if (!displayGradeAndGroup)
        {
            return await GetAllAsync<Characteristic>(parentId);
        }

        using var context = _dbFactory.CreateDbContext();
        
        if (parentId.HasValue)
        {
            return await context.Characteristics.
                                    Include(c => c.Grade).
                                    Include(c => c.CharacteristicsGroup).
                                    Where(t => t.ParentId.HasValue && t.ParentId == parentId).
                                    ToArrayAsync();
        }
        else
        {
            return await context.Characteristics.
                                    Include(c => c.Grade).
                                    Include(c => c.CharacteristicsGroup).
                                    Where(t => !t.ParentId.HasValue).
                                    ToArrayAsync();
        }
    }
}