namespace tictacApp.Data;

public class Project
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public bool IsClosed { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime? FinalizationDate { get; set; }

    public int? ParentProjectId { get; set; }
    public Project? ParentProject { get; set; }
    
    public ICollection<Project>? SubProjects { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
}