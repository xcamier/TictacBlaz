using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class PlannedActivityView: IIdLabel, IDescription, IParent
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public int? StartupQuarter { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsBehind { get; set; }
    public int CompletionPercent { get; set; }
    public bool IsClosed { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime? FinalizationDate { get; set; }
    public ICollection<CommentView>? Comments { get; set; }

    public int? ParentId { get; set; }

    //Calculated properties
    public string BreadcrumbPath { get; set; } = string.Empty;
}