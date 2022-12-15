using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class CommonService
{
    protected IDbContextFactory<TictacDBContext> _dbFactory;

    public CommonService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> HasAtLeastOneItem<T>() where T : class
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Set<T>().AnyAsync();
    }
}