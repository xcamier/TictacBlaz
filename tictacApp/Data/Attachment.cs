namespace tictacApp.Data;

public class Attachment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
    
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
}