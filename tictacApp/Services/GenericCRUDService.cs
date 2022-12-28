using Microsoft.EntityFrameworkCore;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class GenericCRUDService: IGenericCRUDService
{
    protected IDbContextFactory<TictacDBContext> _dbFactory;

    public GenericCRUDService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public TictacDBContext? GetNewDBContext()
    {
        return _dbFactory.CreateDbContext();
    }

    public async Task<bool> HasAtLeastOneItem<T>() where T : class
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Set<T>().AnyAsync();
    }

    public async Task<T[]> GetAllAsync<T>() where T : class
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Set<T>().ToArrayAsync();
    }

    public async Task<T?> FindFromIdAsync<T>(TictacDBContext? dbContext, int id) where T : class, IId
    {    
        if (dbContext is not null && dbContext.Set<T>() is not null)
        {
            return await dbContext.Set<T>().SingleOrDefaultAsync(c => c.Id == id); 
        }

        return null;
    }

    public async Task<bool> AddAsync<T>(T? itemToAdd) where T : class
    {
        if (itemToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Set<T>().Add(itemToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteAsync<T>(T itemToDelete) where T : class
    {
        if (itemToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(itemToDelete);
            
            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<T?> GetFirstAsync<T>() where T : class
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().FirstOrDefaultAsync();
    }
}