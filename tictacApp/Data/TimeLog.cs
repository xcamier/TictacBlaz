namespace tictacApp.Data;

public class TimeLog
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public int TimeSpentInMin { get; set; }
    public string? Description { get; set; }
    public Activity? Activity { get; set; }
    public Objective? Objective { get; set; }
    public Characteristic? Characteristic { get; set; }
    public ICollection<Tag>? Tags { get; set; }

    //Calculated fields, for display only
    public string TimeSpentInHHMM { get; set; } = string.Empty;
    public TimeSpan? TimeSpan { get; set; }
    public string ProjectsAsText { get; set; } = "Select Project...";
    public string ObjectivesAsText { get; set; } = "Select Objective...";
    public string CharacteristicsAsText { get; set; } = "Select Behavior...";
}
