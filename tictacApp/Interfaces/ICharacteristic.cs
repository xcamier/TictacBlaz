using System.Collections.ObjectModel;
using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ICharacteristic<T>
{
    ObservableCollection<T>? Characteristics { get; set; }
    IList<KeyValuePair<int, string>> CharacteristicsAsText { get; set; }
}

public interface ICharacteristics
{
    ICollection<Characteristic> Characteristics { get; set; }    
}