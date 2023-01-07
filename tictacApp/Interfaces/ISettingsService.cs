using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ISettingsService: ISettingsCRUDService
{
    Setting? FindSetting(Setting[] dbSettings, string key);
    int? GetSettingAsInteger(Setting[] dbSettings, string key);
    PlannedActivitySettingsView MapPlannedActivitySettingsToView(Setting[] dbSettings);
}