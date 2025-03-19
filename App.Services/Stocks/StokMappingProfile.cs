using App.Repositories.Entities;
using App.Services.Stocks.DTO;
using AutoMapper;
namespace App.Services.Products
{
    public class StokMappingProfile : Profile
    {
        public StokMappingProfile()
        {
            CreateMap<CreateStockRequest, Stock>().ForMember(dest => dest.MalKodu, opt => opt.MapFrom(src => src.MalKodu)).ReverseMap();
            CreateMap<UpdateStockRequest, Stock>().ForMember(dest => dest.MalKodu, opt => opt.MapFrom(src => src.MalKodu)).ReverseMap();
        }
    }
}
