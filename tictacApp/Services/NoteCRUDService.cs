using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class NoteCRUDService: GenericCRUDService, INoteCRUDService
{
    public NoteCRUDService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public async Task<Note[]> GetAllAsync(TictacDBContext dBContext, bool showClosedOnly) 
    {
        var query = dBContext.Notes.
                        Where(n => n.IsClosed == showClosedOnly);

        return await query.ToArrayAsync();
    }
}