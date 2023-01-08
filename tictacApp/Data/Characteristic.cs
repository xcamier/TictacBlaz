using tictacApp.Interfaces;

namespace tictacApp.Data;

public class Characteristic: IIdLabel, IDescription, IParent, IIsClosed
{  
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }

    public bool IsClosed { get; set; }

    public int? GradeId { get; set; }
    public Grade? Grade { get; set; }

    public int? CharacteristicsGroupId { get; set; }
    public CharacteristicsGroup? CharacteristicsGroup { get; set; }

    public int? ParentId { get; set; }
    public Characteristic? ParentCharacteristic { get; set; }

    public ICollection<Characteristic>? SubCharacteristics { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
    public ICollection<Observation>? Observations { get; set; }
}
