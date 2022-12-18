namespace tictacApp.Interfaces;

public interface IIdLabel: IId
{
    string? Label { get; set; }
}