using tictacApp.Interfaces;
using tictacApp.Services;
using tictacApp.Data;

namespace tictacApp.ViewModels;

public class ObservationView : TimelogObservation, IId, IDescription, ICharacteristic<CharacteristicView>
{
    public DateTime? ObservationDate { get; set; }
    public bool IsPositive { get; set; }
    public string? Evidences { get; set; }

    public int ActorId { get; set; }

    public ObservationView(GenericCRUDServiceWithParents<Characteristic> characteristicsService): base(characteristicsService)
    {
    }
}
