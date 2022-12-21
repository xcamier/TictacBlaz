using tictacApp.Interfaces;

namespace tictacApp.Data;

public class TimeLog : IId
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public int TimeSpentInMin { get; set; }
    public string? Description { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public int? ObjectiveId { get; set; }
    public Objective? Objective { get; set; }

    public ICollection<Characteristic>? Characteristics { get; set; } = new List<Characteristic>();
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();

    //Calculated fields, for display only
    public string TimeSpentInHHMM { get; set; } = string.Empty;
    public TimeSpan? TimeSpan { get; set; } 
    
    public string ProjectsAsText { get; set; } = string.Empty;
    public string ObjectivesAsText { get; set; } = string.Empty;
    public IList<KeyValuePair<int, string>> CharacteristicsAsText { get; set; } = new List<KeyValuePair<int, string>>();
}
