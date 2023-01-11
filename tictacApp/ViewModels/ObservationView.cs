using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class ObservationView : TimelogObservation
{
    public DateTime? ObservationDate { get; set; }
    public bool IsPositive { get; set; }
    public string? Evidences { get; set; }
    public int SelectedRating { get; set; } = 0;

    public int ActorId { get; set; }

    public ObservationView(IGenericCRUDServiceWithParents characteristicsService): base(characteristicsService)
    {
    }
}
