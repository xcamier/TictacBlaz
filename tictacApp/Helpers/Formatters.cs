using tictacApp.ViewModels;

namespace tictacApp.Helpers;

public class FormattersHelper
{
    public static string MinToHoursAndMinAsString(int minutes)
    {
        TimeSpan ts = TimeSpan.FromMinutes(minutes);

        string res = string.Empty;
        string fmt = "00";
        
        int totalHours = ts.Days * 24;

        if (ts.Hours > 0 || totalHours > 0)
        {
            totalHours += ts.Hours;
            res = string.Concat(res, totalHours.ToString(fmt), "h");
        }

        if (res != string.Empty)
        {
            res = string.Concat(res, " ");
        }
        res = string.Concat(res, ts.Minutes.ToString(fmt), "min");

        return res;
    }

    public static string GetListOfTagsAsString(IEnumerable<TagView> _selectedTags)
    {
        return $"Selected tag{(_selectedTags.Count() > 1 ? "s" : "")}: {string.Join(", ", _selectedTags.Select(x => x.Label))}";
    }

    public static DateTime GetStartDate(DateTime? startDate)
    {
        if (startDate.HasValue)
        {
            return startDate.Value.Date;
        }

        throw new NullReferenceException("The start date shall have a value");
    }

    public static DateTime GetEndDate(DateTime? endDate)
    {
        if (endDate.HasValue)
        {
            return new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);
        }

        throw new NullReferenceException("The end date shall have a value");
    }
}