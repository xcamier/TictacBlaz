namespace tictacApp.Data;

public class Objective: PlannedActivity
{
    public Objective? ParentObjective { get; set; }

    public ICollection<Objective>? SubObjectives { get; set; }
}