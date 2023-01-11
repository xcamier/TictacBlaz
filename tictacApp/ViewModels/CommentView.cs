namespace tictacApp.ViewModels;

public class CommentView
{
    public int Id { get; set; }
    public required DateTime CommentDateTime { get; set; }
    public required string CommentText { get; set; }
    public ICollection<AttachmentView>? Attachments {get; set; }
}