using AutoMapper;
using Service.Data;
using Vehicle_Database_MVC.Data;
using Vehicle_Database_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Database_MVC.Helper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<VehicleMake, VehicleDto>().ReverseMap();
                  
            CreateMap<VehicleModel, VehicleDto>().ReverseMap();

            var configurationMake = new MapperConfiguration(cfg =>
            cfg.CreateProjection<VehicleMake, VehicleDto>()
            .ForMember(dto => dto.Id, conf => conf.MapFrom(ol => ol.Id)));

            var configurationModel = new MapperConfiguration(cfg =>
            cfg.CreateProjection<VehicleModel, VehicleDto>()
            .ForMember(dto => dto.Id, conf => conf.MapFrom(ol => ol.Id))
            .ForMember(dto=> dto.MakeId, conf=>conf.MapFrom(ol => ol.VehicleMake.Id)));
            
        }
    }
}
