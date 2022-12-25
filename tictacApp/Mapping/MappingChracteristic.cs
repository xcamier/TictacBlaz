using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingChracteristic : Profile
{
    public MappingChracteristic()
    {
        CreateMap<Characteristic, CharacteristicView>();  
    }
}