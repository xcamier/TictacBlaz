using tictacApp.Interfaces;

namespace tictacApp.Data;

public class Note: IId, IDescription, IIsClosed
{
    public int Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public string Description { get; set; }
    public bool IsClosed { get; set; }
}
