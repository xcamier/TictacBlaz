namespace tictacApp.Interfaces;

public interface IGenericCRUDServiceWithParents: IGenericCRUDService
{
    Task<T[]> GetAllAsync<T>(int? parentId) where T: class, IParent;
    Task<KeyValuePair<int, string?>[]> GetParentsAsync<T>(int? id) where T: class, IIdLabel, IParent;
}