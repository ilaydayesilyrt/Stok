using App.Repositories.Entities;
using App.Services.StokHareketleri.DTO;
using AutoMapper;
namespace App.Services.Products
{
    public class StokHareketMappingProfile : Profile
    {
        public StokHareketMappingProfile()
        {
            CreateMap<StokHareket, StokHareketDto>().ReverseMap();
            CreateMap<CreateStokHareketRequest, StokHareket>().ReverseMap();
            CreateMap<UpdateStokHareketRequest, StokHareket>().ReverseMap();
        }
    }
}
