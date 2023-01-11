using AutoMapper;
using tictacApp.Data;
using tictacApp.ViewModels;

public class MappingCharacteristic : Profile
{
    public MappingCharacteristic()
    {
        CreateMap<Characteristic, CharacteristicView>();  
    }
}