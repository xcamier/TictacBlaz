using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingNote : Profile
{
    public MappingNote()
    {
        CreateMap<Note, NoteView>(); 
        CreateMap<NoteView, Note>(); 
    }
}