namespace tictacApp.Interfaces;

public interface IGenericCRUDServiceWithParents: IGenericCRUDService
{
    Task<T[]> GetAllAsync<T>(int? parentId, bool showClosedOnly) where T: class, IParent, IIsClosed;
    Task<KeyValuePair<int, string?>[]> GetParentsAsync<T>(int? id) where T: class, IIdLabel, IParent;
}