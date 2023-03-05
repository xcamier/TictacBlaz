using tictacApp.Data;
using tictacApp.DataAccess;

namespace tictacApp.Interfaces;

public interface INoteCRUDService: IGenericCRUDService
{
    Task<Note[]> GetAllAsync(TictacDBContext dBContext, bool showClosedOnly);
}