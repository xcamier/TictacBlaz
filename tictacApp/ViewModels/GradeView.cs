using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class GradeView: IIdLabel
{
    public int Id { get; set; }
    public string? Label { get; set; }
}