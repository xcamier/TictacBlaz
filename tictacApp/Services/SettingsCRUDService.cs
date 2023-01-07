using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Interfaces;

namespace tictacApp.Services;

public class SettingsCRUDService: ISettingsCRUDService
{
    protected IDbContextFactory<TictacDBContext> _dbFactory;

    public SettingsCRUDService(IDbContextFactory<TictacDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<Setting[]> GetAllAsync() 
    {
        using var context = _dbFactory.CreateDbContext();
        
        return await context.Settings.ToArrayAsync();
    }

    public async Task PerformDbUpdates(IEnumerable<Setting> settingsToCreate, 
                                        IEnumerable<Setting> settingsToUpdate, 
                                        IEnumerable<Setting> settingsToDelete)
    {            
        using var context = _dbFactory.CreateDbContext();

        bool hasDbOperations = false;

        bool hasAddOperations = ProcessAdding(context, settingsToCreate);
        bool hasUpdateOperations = await ProcessUpdating(context, settingsToUpdate);
        bool hasDeleteOperations = await ProcessDeleting(context, settingsToDelete);        

        hasDbOperations = hasAddOperations || hasUpdateOperations || hasDeleteOperations;
        if (hasDbOperations)
        {
            await context.SaveChangesAsync();
        }
    }

    private bool ProcessAdding(TictacDBContext context, IEnumerable<Setting> settingsToCreate)
    {   
        if (settingsToCreate is null)
            return false;

        foreach (Setting setting in settingsToCreate)
        {
            context.Settings.Add(setting);
        }

        return settingsToCreate.Any();
    }

    private async Task<bool> ProcessUpdating(TictacDBContext context, IEnumerable<Setting> settingsToUpdate)
    {
        if (settingsToUpdate is null)
            return false;

        foreach (Setting setting in settingsToUpdate)
        {
            Setting? foundSetting = await context.Settings.FindAsync(setting.Key);
            if (foundSetting is not null)
            {
                foundSetting.Value = setting.Value;
            }
            //We keep silent the fact something weird is happening. The setting should have been found
        }

        return settingsToUpdate.Any();
    }

    private async Task<bool> ProcessDeleting(TictacDBContext context, IEnumerable<Setting> settingsToDelete)
    {
        if (settingsToDelete is null)
            return false;

        foreach (Setting setting in settingsToDelete)
        {
            Setting? foundSetting = await context.Settings.FindAsync(setting.Key);
            if (foundSetting is not null)
            {
                context.Remove(foundSetting);
            }
            //We keep silent the fact something weird is happening. The setting should have been found
        }

        return settingsToDelete.Any();
    }
}