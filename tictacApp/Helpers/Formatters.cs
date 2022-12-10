namespace tictacApp.Helpers;

public class FormattersHelper
{
    public static string MinToString(int minutes)
    {
        TimeSpan ts = TimeSpan.FromMinutes(minutes);

        string res = string.Empty;
        string fmt = "00";
        
        if (ts.Days > 0)
        {
            res = $"{ts.Days.ToString(fmt)}d";
        }

        if (ts.Hours > 0)
        {
            if (res != string.Empty)
            {
                res = string.Concat(res, " ");
            }

            res = string.Concat(res, ts.Hours.ToString(fmt), "h");
        }

        if (res != string.Empty)
        {
            res = string.Concat(res, " ");
        }
        res = string.Concat(res, ts.Minutes.ToString(fmt), "min");

        return res;
    }
}