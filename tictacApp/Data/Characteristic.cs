namespace tictacApp.Data;

public class Characteristic
{  
    public int Id { get; set; }
    public string? Description1 { get; set; }
    public string? Description2 { get; set; }
    public string? Color { get; set; }

    public bool IsClosed { get; set; }

    public int? GradeId { get; set; }
    public Grade? Grade { get; set; }

    public int? CharacteristicsGroupId { get; set; }
    public CharacteristicsGroup? CharacteristicsGroup { get; set; }

    public int? ParentCharacteristicId { get; set; }
    public Characteristic? ParentCharacteristic { get; set; }

    public ICollection<Characteristic>? SubCharacteristics { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
}
