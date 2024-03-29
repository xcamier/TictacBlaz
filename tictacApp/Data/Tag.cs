using  tictacApp.Interfaces;

namespace tictacApp.Data;

public class Tag: IIdLabel
{
    public int Id { get; set; }
    public string? Label { get; set; }

    public ICollection<TimeLog>? TimeLogs { get; set; }

    public ICollection<Observation>? Observations { get; set; }


    //Usefull for the tags multiselect component
    //TODO: to challenge as now, displayed with the view model
    public override bool Equals(object o) {
        var other = o as Tag;
        return other?.Label == Label;
    }

    // Note: this is important so the select can compare pizzas
    public override int GetHashCode() => Label.GetHashCode();
}