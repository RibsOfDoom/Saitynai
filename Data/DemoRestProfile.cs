using AutoMapper;
using L1_Zvejyba.Data.DTOs.Bodies;
using L1_Zvejyba.Data.DTOs.Cities;
using L1_Zvejyba.Data.DTOs.Fish;
using L1_Zvejyba.Data.Entities;

namespace L1_Zvejyba.Data
{
    public class DemoRestProfile : Profile
    {
        public DemoRestProfile()
        {
            CreateMap<City, CityDTO>();
            CreateMap<CreateCityDTO, City>();
            CreateMap<UpdateCityDTO, City>();

            CreateMap<Body, BodyDTO>();
            CreateMap<CreateBodyDTO, Body>();
            CreateMap<UpdateBodyDTO, Body>();

            CreateMap<Fish, FishDTO>();
            CreateMap<CreateFishDTO, Fish>();
            CreateMap<UpdateFishDTO, Fish>();
        }
    }
}
