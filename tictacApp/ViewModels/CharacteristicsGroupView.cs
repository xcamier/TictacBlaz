using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class CharacteristicsGroupView: IIdLabel
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public bool IsClosed { get; set; }
}