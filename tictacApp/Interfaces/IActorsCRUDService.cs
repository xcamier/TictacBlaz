using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface IActorsCRUDService: IGenericCRUDService
{
    Task<Actor[]> GetAllAsync(bool showInactivesOnly);
}