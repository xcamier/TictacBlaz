using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class NoteView: IId, IDescription, IIsClosed
{
    public int Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public string Description { get; set; }
    public bool IsClosed { get; set; }
    public bool IsNew { get; set; }
    public bool IsUpToDateInDb { get; set; }
}
