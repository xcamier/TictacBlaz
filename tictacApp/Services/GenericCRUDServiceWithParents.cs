using Microsoft.EntityFrameworkCore;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class GenericCRUDServiceWithParents: GenericCRUDService, IGenericCRUDServiceWithParents
{
    public GenericCRUDServiceWithParents(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<T[]> GetAllAsync<T>(int? parentId, bool showClosedOnly) where T: class, IParent, IIsClosed
    {
        using var context = _dbFactory.CreateDbContext();
        
        var query = context.Set<T>().
                        Where(t => t.ParentId.HasValue == parentId.HasValue && t.IsClosed == showClosedOnly);

        if (parentId.HasValue)
        {
            query = query.Where(t => t.ParentId == parentId);
        }

        return await query.ToArrayAsync();
    }

    public async Task<KeyValuePair<int, string?>[]> GetParentsAsync<T>(int? id) where T: class, IIdLabel, IParent
    {
        List<KeyValuePair<int, string?>> parents = new List<KeyValuePair<int, string?>>();

        using var context = _dbFactory.CreateDbContext();

        int? idToSearch = id;
        while (idToSearch.HasValue)
        {
            T? foundEntity = await context.Set<T>().SingleOrDefaultAsync(t => t.Id == idToSearch.Value);
            if (foundEntity is not null)
            {
                KeyValuePair<int, string?> item = new KeyValuePair<int, string?>(foundEntity.Id, foundEntity.Label);
                parents.Insert(0, item);

                idToSearch = foundEntity.ParentId;
            }
            else
            {
                idToSearch = null;
            }
        }

        return parents.ToArray();
    }
}