using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingTag : Profile
{
    public MappingTag()
    {
        CreateMap<Tag, TagView>(); 
    }
}