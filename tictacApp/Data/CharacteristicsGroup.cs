namespace tictacApp.Data;

public class CharacteristicsGroup
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public bool IsClosed { get; set; }

    public ICollection<Characteristic>? Characteristics { get; set; }
}