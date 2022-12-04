namespace tictacApp.Data;

public class Characteristic
{  
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public Characteristic? ParentCharacteristic { get; set; }

    public ICollection<Characteristic>? SubCharacteristics { get; set; }
    public ICollection<TimeLog>? TimeLogs { get; set; }
}
