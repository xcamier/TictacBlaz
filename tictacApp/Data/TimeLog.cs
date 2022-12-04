namespace tictacApp.Data;

public class TimeLog
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public int TimeSpentInMin { get; set; }
    public string? Description { get; set; }
    public Activity? Activity { get; set; }
    public Objective? Objective { get; set; }
    public Characteristic? Characteristic { get; set; }

    //Calculated fields, for display only
    public string TimeSpentInHHMM { get; set; } = string.Empty;
}
