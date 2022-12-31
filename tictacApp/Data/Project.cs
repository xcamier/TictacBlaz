namespace tictacApp.Data;

public class Project: PlannedActivity
{
    public Project? ParentProject { get; set; }
    public ICollection<Project>? SubProjects{ get; set; }
}