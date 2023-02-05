public class ActivityRiskHelper
{
    public static bool IsInZone(DateTime? targetDate, int? low, int? high)
    {
        if (!targetDate.HasValue || !low.HasValue || !high.HasValue) 
            return false;

        DateTime now = DateTime.Now.Date; 
        double remainingDays = CalculateRemainingDays(now, targetDate.Value);

        return (remainingDays >= low && remainingDays <= high);
    }

    public static bool IsTooLate(DateTime? targetDate, int? orangeLow)
    {
        if (!targetDate.HasValue)
            return false;

        DateTime now = DateTime.Now.Date; 
        double remainingDays = CalculateRemainingDays(now, targetDate.Value);

        if (orangeLow.HasValue)
        {
            return remainingDays < orangeLow;
        }

        return targetDate.Value <= now;
    }

    public static bool IsInDangerZone(DateTime? targetDate, PlannedActivitySettingsView activitiesSettings)
    {
        return IsInZone(targetDate, activitiesSettings.OrangeLow, activitiesSettings.OrangeHigh);
    }

    public static bool IsInGreenZone(DateTime? targetDate, PlannedActivitySettingsView activitiesSettings)
    {
        return IsInZone(targetDate, activitiesSettings.GreenLow, activitiesSettings.GreenHigh);
    }

    private static double CalculateRemainingDays(DateTime now, DateTime targetDate)
    {
        DateTime end = targetDate.Date.AddDays(1).AddSeconds(-1);
        TimeSpan remaining = end - now;

        return Math.Round(remaining.TotalDays);
    }
}