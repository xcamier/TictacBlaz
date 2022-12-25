using  tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class TagView: IIdLabel
{
    public int Id { get; set; }
    public string? Label { get; set; }


    //usefull for the blazor multiselect control
    public override bool Equals(object o) {
        var other = o as TagView;
        return other?.Label == Label;
    }

    // Note: this is important so the select can compare pizzas
    public override int GetHashCode() => Label.GetHashCode();
}