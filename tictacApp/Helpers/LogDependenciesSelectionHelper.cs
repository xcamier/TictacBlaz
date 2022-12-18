namespace tictacApp.Helpers;

public enum SelectionSource
{
    None,
    Project,
    Objective,
    Characteristic
}

public class LogDependenciesSelectionHelper
{
    public static SelectionSource ComesFromSelection(string uri)
    {   
        if (uri.EndsWith("FromProjectSelection"))
        {
            return SelectionSource.Project;
        }
        else if (uri.EndsWith("FromObjectiveSelection"))
        {
            return SelectionSource.Objective;
        }
        else if (uri.EndsWith("FromCharacteristicSelection"))
        {
            return SelectionSource.Characteristic;
        }

        return SelectionSource.None;
    }
}