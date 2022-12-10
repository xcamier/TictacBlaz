using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class TagsService
{
    IDbContextFactory<TictacDBContext> _dbFactory;

    public TagsService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<Tag[]> GetTagsAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Tags.OrderBy(t => t.Label).ToArrayAsync();
    }

    public async Task<Tag?> FindTagFromIdAsync(TictacDBContext? dbContext, int tagId)
    {    
        if (dbContext is not null && dbContext.Tags is not null)
        {
            return await dbContext.Tags.SingleOrDefaultAsync(c => c.Id == tagId); 
        }

        return null;
    }

    public async Task<bool> AddTagAsync(Tag? tagToAdd)
    {
        if (tagToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Tags?.Add(tagToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteTagAsync(Tag tagToDelete)
    {
        if (tagToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(tagToDelete);
            
            return await context.SaveChangesAsync() > 0;

        }

        return false;
    }
}