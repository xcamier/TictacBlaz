namespace tictacApp.Data;

public class Tag
{
    public int Id { get; set; }
    public string? Label { get; set; }

    public ICollection<TimeLog>? TimeLogs { get; set; }

        public override bool Equals(object o) {
            var other = o as Tag;
            return other?.Id == Id;
        }
}