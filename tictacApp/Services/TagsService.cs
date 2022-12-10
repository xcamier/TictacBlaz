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

    public async Task<Tag[]> GetTagsAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Tags.OrderBy(t => t.Label).ToArrayAsync();
    }
}