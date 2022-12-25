using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class ProjectView: IIdLabel, IDescription, IParent
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public bool IsClosed { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime? FinalizationDate { get; set; }

    public int? ParentId { get; set; }
}