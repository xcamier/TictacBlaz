namespace tictacApp.Data;

public class Tag
{
    public int Id { get; set; }
    public string? Label { get; set; }

    public ICollection<TimeLog>? TimeLogs { get; set; }
}