using System.Collections.ObjectModel;
using System.Collections.Specialized;
using tictacApp.Data;
using tictacApp.Helpers;
using tictacApp.Interfaces;
using tictacApp.Services;

namespace tictacApp.ViewModels;

public class TimelogObservation: IId, IDescription, ICharacteristic<CharacteristicView>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public ObservableCollection<CharacteristicView>? Characteristics { get; set; } = new ObservableCollection<CharacteristicView>();
    public ICollection<TagView>? Tags { get; set; } = new List<TagView>();

    //Calculated field
    public IList<KeyValuePair<int, string>> CharacteristicsAsText { get; set; } = new List<KeyValuePair<int, string>>();

    private IGenericCRUDServiceWithParents _characteristicsService;

    public TimelogObservation(IGenericCRUDServiceWithParents characteristicsService)
    {
        _characteristicsService = characteristicsService;

        Characteristics.CollectionChanged +=  CharacteristicSelectionChanged;
    }

   private async void CharacteristicSelectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e != null)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                await AddSelectedCharacteristic(e);
                break;

                case NotifyCollectionChangedAction.Remove:
                RemoveCharacteristic(e);
                break;

                case NotifyCollectionChangedAction.Reset:
                return;

                default:
                    throw new NotImplementedException("Change on the characteristics collection is not managed");
            }
        }
    }

    private async Task AddSelectedCharacteristic(NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                CharacteristicView cv = (CharacteristicView)item;

                string asText = await BreadcrumbHelper.BuildSimpifiedBreadcrumb<Characteristic>(_characteristicsService, cv.Id);

                KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(cv.Id, asText);
                CharacteristicsAsText.Add(kvp);
            }
        }
    }

    private void RemoveCharacteristic(NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
        {
            foreach (var item in e.OldItems)
            {
                CharacteristicView cv = (CharacteristicView)item;
                var toto = CharacteristicsAsText.Single(cat => cat.Key == cv.Id);
                CharacteristicsAsText.Remove(toto);
            }
        }
    }
}