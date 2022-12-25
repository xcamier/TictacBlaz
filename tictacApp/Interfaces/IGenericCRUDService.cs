using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface IGenericCRUDService<T>
{
    TictacDBContext? GetNewDBContext();
    Task<bool> HasAtLeastOneItem<U>() where U : class;
    Task<T[]> GetAllAsync();
    Task<T?> FindFromIdAsync(TictacDBContext? dbContext, int id);
    Task<bool> AddAsync(T? itemToAdd);
    Task<bool> DeleteAsync(T itemToDelete);
    Task<T?> GetFirstAsync();
}