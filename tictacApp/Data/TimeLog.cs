namespace tictacApp.Data;

public class TimeLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TimeSpentInMin { get; set; }
    public string? Description { get; set; }
    public Objective? Objective { get; set; }
    public Characteristic? Characteristic { get; set; }
}
