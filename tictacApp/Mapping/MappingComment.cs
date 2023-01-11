using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingComment : Profile
{
    public MappingComment()
    {
        CreateMap<Comment, CommentView>(); 
        CreateMap<Attachment, AttachmentView>();
        CreateMap<CommentView, Comment>();
        CreateMap<AttachmentView, Attachment>();

        CreateMap<CommentView, CommentView>();
    }
}