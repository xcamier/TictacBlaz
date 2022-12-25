using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingObservation : Profile
{
    public MappingObservation()
    {
        CreateMap<Observation, ObservationView>();  //Map from Observation Object to ObservationView Object
        CreateMap<ObservationView, Observation>().
            ForMember(dest => dest.Characteristics, act => act.Ignore()).
            ForMember(dest => dest.Tags, act => act.Ignore()); 
    }
}