using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ISettingsCRUDService
{
    Task<Setting[]> GetAllAsync();

    Task PerformDbUpdates(IEnumerable<Setting> settingsToCreate, 
                            IEnumerable<Setting> settingsToUpdate, 
                            IEnumerable<Setting> settingsToDelete);
}