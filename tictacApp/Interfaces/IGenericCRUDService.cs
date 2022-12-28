using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface IGenericCRUDService
{
    TictacDBContext? GetNewDBContext();
    Task<bool> HasAtLeastOneItem<U>() where U : class;
    Task<T[]> GetAllAsync<T>() where T : class;
    Task<T?> FindFromIdAsync<T>(TictacDBContext? dbContext, int id) where T : class, IId;
    Task<bool> AddAsync<T>(T? itemToAdd) where T : class;
    Task<bool> DeleteAsync<T>(T itemToDelete) where T : class;
    Task<T?> GetFirstAsync<T>() where T : class;
}