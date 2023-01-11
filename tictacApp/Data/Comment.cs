using tictacApp.Interfaces;

namespace tictacApp.Data;

public class Comment: IId
{
    public int Id { get; set; }
    public required DateTime CommentDateTime { get; set; }
    public required string CommentText { get; set; }
    public ICollection<Attachment>? Attachments {get; set; }

    public int PlannedActivityId { get; set; }
    public required PlannedActivity PlannedActivity { get; set; }
}