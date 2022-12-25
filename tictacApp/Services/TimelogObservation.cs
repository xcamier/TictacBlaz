using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class TimelogObservation<T>
{
    protected IDbContextFactory<TictacDBContext> _dbFactory;

    public TimelogObservation(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetNewDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<T?> FindFromIdAsync<T>(TictacDBContext context, int id) 
                                                    where T: class, IId, ICharacteristics, ITags
    {    
        if (context is not null && context.TimeLogs is not null)
        {
            return await context.Set<T>().
                                Include(t => t.Characteristics).
                                Include(t => t.Tags).
                                SingleOrDefaultAsync(c => c.Id == id); 
        }

        return null;
    }

    public async Task<T?> FindFromIdAsync<T>(int id) where T: class, IId, ICharacteristics, ITags
    {    
        using var context = _dbFactory.CreateDbContext();

        return await FindFromIdAsync<T>(context, id);
    }

    public async Task<bool> AddAsync<T>(T? entityToAdd,
                                        IEnumerable<int> characteristicsToLink, IEnumerable<int> tagsToLink)  
                                        where T: class, ICharacteristics, ITags
    {
        if (entityToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
        
            if (context is not null && context.Observations is not null)
            {
                List<Characteristic> selectedCharInDb = 
                                        context.Characteristics.Where(c => characteristicsToLink.Contains(c.Id)).ToList();
                entityToAdd.Characteristics = selectedCharInDb;

                List<Tag> selectedTagsInDb = context.Tags.Where(t => tagsToLink.Contains(t.Id)).ToList();
                entityToAdd.Tags = selectedTagsInDb;

                context.Set<T>().Add(entityToAdd);

                return await context.SaveChangesAsync() > 0;
            }
        }

        return false;
    }

    public async Task<bool> UpdateAsync<T>(TictacDBContext context, T? entityToUpdate, 
                                                IEnumerable<int> charsToLink, IEnumerable<int> charsToUnlink,
                                                IEnumerable<int> tagsToLink, IEnumerable<int> tagsToUnlink)
                                                where T: class, ICharacteristics, ITags
    {
        if (context is not null && entityToUpdate is not null && context.TimeLogs is not null)
        {
            RemoveThenAddDependencies<Characteristic>(context, entityToUpdate.Characteristics, charsToLink, charsToUnlink);
            RemoveThenAddDependencies<Tag>(context, entityToUpdate.Tags, tagsToLink, tagsToUnlink);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    private static void RemoveThenAddDependencies<T>(TictacDBContext context, ICollection<T> collectionToUpdate, 
                                                    IEnumerable<int> toLink, IEnumerable<int> toUnlink) where T: class, IId
    {
        //Makes only one query to retrieve all the T to treat
        List<int> allToTreatId = new List<int>();
        allToTreatId.AddRange(toLink);
        allToTreatId.AddRange(toUnlink);
        List<T> fromDb = context.Set<T>().Where(c => allToTreatId.Contains(c.Id)).ToList();

        //Removal
        List<T> toRemove = new List<T>(fromDb.Where(cdb => toUnlink.Contains(cdb.Id)));
        foreach (T aChar in toRemove)
        {
            collectionToUpdate.Remove(aChar);
        }

        //Adding
        List<T> toAdd = new List<T>(fromDb.Where(cdb => toLink.Contains(cdb.Id)));
        foreach (T aChar in toAdd)
        {
            collectionToUpdate.Add(aChar);
        }
    }
}