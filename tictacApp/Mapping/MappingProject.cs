using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<Project, ProjectView>(); 
        CreateMap<Project, PlannedActivityView>();
    }
}