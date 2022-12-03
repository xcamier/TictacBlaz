namespace tictacApp.Data;

public class Characteristic
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public int ParentId { get; set; }
}
