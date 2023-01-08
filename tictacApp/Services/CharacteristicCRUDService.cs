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

    public async Task<Characteristic[]> GetAllAsync(int? parentId, bool displayGradeAndGroup, bool showClosedOnly)
    {
        if (!displayGradeAndGroup)
        {
            return await GetAllAsync<Characteristic>(parentId, showClosedOnly);
        }

        using var context = _dbFactory.CreateDbContext();
        
        var query = context.Characteristics.
                                Include(c => c.Grade).
                                Include(c => c.CharacteristicsGroup).
                                Where(t => t.IsClosed == showClosedOnly);

        if (parentId.HasValue)
        {
            query =  query.Where(t => t.ParentId.HasValue && t.ParentId == parentId);
        }

        return await query.ToArrayAsync();
    }
}