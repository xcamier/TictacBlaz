using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingActor : Profile
{
    public MappingActor()
    {
        CreateMap<Actor, ActorView>();  
    }
}