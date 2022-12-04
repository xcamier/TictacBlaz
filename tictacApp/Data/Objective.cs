namespace tictacApp.Data;

public class Objective
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public Objective? ParentObjective { get; set; }

    public ICollection<Objective>? SubObjectives { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
}