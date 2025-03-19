using App.Repositories.Enums;
namespace App.Services.StokHareketleri.DTO
{
    public record CreateStokHareketRequest(int Miktar, StokHareketTipi HareketTipi);
}
