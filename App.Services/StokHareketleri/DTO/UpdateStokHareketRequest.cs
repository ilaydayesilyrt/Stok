using App.Repositories.Enums;
namespace App.Services.StokHareketleri.DTO
{
    public record UpdateStokHareketRequest(Guid Id, int Miktar, StokHareketTipi StokHareketTipi);
}
