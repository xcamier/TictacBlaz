namespace tictacApp.Data;

public class Activity
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public Activity? ParentActivity { get; set; }

    public ICollection<Activity>? SubActivities { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
}