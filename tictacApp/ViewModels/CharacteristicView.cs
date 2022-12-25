using tictacApp.Interfaces;

namespace tictacApp.ViewModels;

public class CharacteristicView: IIdLabel, IDescription, IParent
{  
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }

    public bool IsClosed { get; set; }

    public int? GradeId { get; set; }

    public int? CharacteristicsGroupId { get; set; }

    public int? ParentId { get; set; }
}
