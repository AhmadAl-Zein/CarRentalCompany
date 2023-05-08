using AutoMapper;
using CarRentalCompany.Dto;
using CarRentalCompany.Models;

namespace CarRentalCompany
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDto, Car>().ReverseMap();
        }
    }
}
