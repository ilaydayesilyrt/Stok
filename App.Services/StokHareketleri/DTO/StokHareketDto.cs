using App.Repositories.Enums;
namespace App.Services.StokHareketleri.DTO
{
    public class StokHareketDto()
    {
        public int Miktar { get; set; }
        public StokHareketTipi StokHareketTipi { get; set; }
    }
}
