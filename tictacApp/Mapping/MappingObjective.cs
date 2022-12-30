using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingObjective : Profile
{
    public MappingObjective()
    {
        CreateMap<Objective, ObjectiveView>(); 
        CreateMap<Objective, PlannedActivityView>();
    }
}