namespace tictacApp.Data;

public class Grade
{
    public int Id { get; set; }
    public string? Label { get; set; }

    public ICollection<Characteristic>? Characteristics { get; set; }
    public ICollection<Actor>? Actors { get; set; }
}