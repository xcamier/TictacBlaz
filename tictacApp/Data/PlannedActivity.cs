using tictacApp.Interfaces;

namespace tictacApp.Data;

public abstract class PlannedActivity: IIdLabel, IDescription, IParent
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsClosed { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime? FinalizationDate { get; set; }

    public int? ParentId { get; set; }

    public ICollection<TimeLog>? TimeLogs { get; set; }
}