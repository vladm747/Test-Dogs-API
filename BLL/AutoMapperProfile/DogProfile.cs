using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.AutoMapperProfile
{
    public class DogProfile: Profile
    {
        public DogProfile()
        {
            CreateMap<Dog, DogUpdateDTO>().ReverseMap();
            CreateMap<Dog, DogDTO>().ReverseMap();
        }
    }
}
