using tictacApp.Interfaces;

namespace tictacApp.Data;

public class Actor: IId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool UseAsDefault { get; set; }
    public bool IsInactive { get; set; }

    public int DefaultGradeId { get; set; }
    public Grade DefaultGrade { get; set; }
}