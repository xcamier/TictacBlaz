using Microsoft.EntityFrameworkCore;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class GenericCRUDService<T> where T: class, IId
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

    public async Task<T[]> GetAllAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Set<T>().ToArrayAsync();
    }

    public async Task<T?> FindFromIdAsync(TictacDBContext? dbContext, int id)
    {    
        if (dbContext is not null && dbContext.Set<T>() is not null)
        {
            return await dbContext.Set<T>().SingleOrDefaultAsync(c => c.Id == id); 
        }

        return null;
    }

    public async Task<bool> AddAsync(T? itemToAdd)
    {
        if (itemToAdd != null)
        {
            using var context = _dbFactory.CreateDbContext();
            
            context.Set<T>().Add(itemToAdd);

            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(T itemToDelete)
    {
        if (itemToDelete != null)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Remove(itemToDelete);
            
            return await context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<T?> GetFirstAsync()
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Set<T>().FirstOrDefaultAsync();
    }
}