using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingTimelog : Profile
{
    public MappingTimelog()
    {
        //Map from TimeLog Object to Timelogview Object
        CreateMap<TimeLog, TimeLogView>();
        
        //Map from TimeLogView Object to Timelog Object
        CreateMap<TimeLogView, TimeLog>().
            ForMember(dest => dest.Characteristics, act => act.Ignore()).
            ForMember(dest => dest.Tags, act => act.Ignore()); 
    }
}