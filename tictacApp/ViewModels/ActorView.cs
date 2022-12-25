using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class ActorView: IId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool UseAsDefault { get; set; }
    public bool IsInactive { get; set; }

    public int DefaultGradeId { get; set; }
}