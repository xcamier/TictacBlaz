using Microsoft.EntityFrameworkCore;
using tictacApp.Data;
using tictacApp.DataAccess;
using tictacApp.Helpers;
using tictacApp.Interfaces;

namespace tictacApp.Services;

//Separation of the logic of the CRUD from additional treatments
public class SettingsService: SettingsCRUDService, ISettingsService
{
    public SettingsService(IDbContextFactory<TictacDBContext> dbFactory): base(dbFactory)
    {
    }

    public Setting? FindSetting(Setting[] dbSettings, string key)
    {
        return dbSettings.FirstOrDefault(s => s.Key == key);
    }

    public int? GetSettingAsInteger(Setting[] dbSettings, string key)
    {
        Setting? setting = FindSetting(dbSettings, key);
        if (setting is not null)
        {
            int convResult;
            bool conversionOk = int.TryParse(setting.Value, out convResult);

            if (conversionOk)
            {
                return convResult;
            }
        }

        return null;
    }

    public PlannedActivitySettingsView MapPlannedActivitySettingsToView(Setting[] dbSettings)
    {
        PlannedActivitySettingsView settings = new PlannedActivitySettingsView()
        {
            OrangeLow =  GetSettingAsInteger(dbSettings, SettingsKeys.OrangeLow),
            OrangeHigh = GetSettingAsInteger(dbSettings, SettingsKeys.OrangeHigh),
            GreenLow = GetSettingAsInteger(dbSettings, SettingsKeys.GreenLow),
            GreenHigh = GetSettingAsInteger(dbSettings, SettingsKeys.GreenHigh)
        };

        return settings;
    }
}