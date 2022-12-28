using Microsoft.EntityFrameworkCore;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class GenericCRUDServiceWithParents: GenericCRUDService, IGenericCRUDServiceWithParents
{
    public GenericCRUDServiceWithParents(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<T[]> GetAllAsync<T>(int? parentId) where T: class, IParent
    {
        using var context = _dbFactory.CreateDbContext();
        
        if (parentId.HasValue)
        {
            return await context.Set<T>().
                                    Where(t => t.ParentId.HasValue && t.ParentId == parentId).
                                    ToArrayAsync();
        }
        else
        {
            return await context.Set<T>().
                                    Where(t => !t.ParentId.HasValue).
                                    ToArrayAsync();
        }
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