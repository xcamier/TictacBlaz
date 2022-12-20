using Microsoft.EntityFrameworkCore;
using tictacApp.DataAccess;

namespace tictacApp.Services;

public class MigrationService
{
    private readonly IServiceScopeFactory? _serviceScopeFactory;

    public MigrationService(IServiceScopeFactory? serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task MigrateAsync()
    {
        using (var scope = _serviceScopeFactory?.CreateScope())
        {
            var db = scope?.ServiceProvider.GetService<TictacDBContext>();

            if (db != null)
            {
                await db.Database.MigrateAsync();
            }

        }
    }
}