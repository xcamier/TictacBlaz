using tictacApp.Data;

namespace tictacApp.Services;

public class ItemSelectionService<T> where T: class
{
    public TimeLog? EntityBackup { get; set; }
    public bool IsAdd { get; set; }
    public IList<T> Selection { get; set; } = new List<T>();
    public bool HasSelected { get; set; }
    public bool ComesFromSelection { get; set; }


    public void Reset()
    {
        ComesFromSelection = false;
        HasSelected = false;
        EntityBackup = null;
        IsAdd = false;
        Selection.Clear();
    }
}