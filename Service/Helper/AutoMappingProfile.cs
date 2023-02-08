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
            CreateMap<VehicleMake, VehicleMakeDto>()
                .ForMember(vm => vm.Id, o => o.MapFrom(src => src.Id))
                .ForMember(vm => vm.VehicleName, o => o.MapFrom(src => src.VehicleName))
                .ForMember(vm => vm.VehicleAbrv, o => o.MapFrom(src => src.VehicleAbrv))
                .ReverseMap();
            CreateMap<VehicleModel, VehicleModelDto>()
              .ForMember(vm => vm.Id, o => o.MapFrom(src => src.Id))
              .ForMember(vm => vm.MakeId, o => o.MapFrom(src => src.MakeId))
              .ForMember(vm => vm.VehicleName, o => o.MapFrom(src => src.VehicleName))
              .ForMember(vm => vm.VehicleAbrv, o => o.MapFrom(src => src.VehicleAbrv))
              .ReverseMap();
        }
    }
}
