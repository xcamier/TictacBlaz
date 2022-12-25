using tictacApp.Interfaces;

namespace tictacApp.Data;

public class Observation : IId, IDescription, ICharacteristics, ITags
{
    public int Id { get; set; }
    public DateTime? ObservationDate { get; set; }
    public string Description { get; set; }
    public bool IsPositive { get; set; }
    public string? Evidences { get; set; }

    public int ActorId { get; set; }
    public Actor Actor { get; set; }
    
    public ICollection<Characteristic>? Characteristics { get; set; } = new List<Characteristic>();
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
}
