using AutoMapper;
using NorthwindApi.Models;

namespace NorthwindApi.Configurations
{

    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Person, CreatePersonDTO>().ReverseMap();
        }

    }

}
