using App.Repositories.Enums;
namespace App.Repositories.Entities
{
   public class StokHareket : BaseEntity<Guid>
   {
        public int Miktar { get; set; }
        public StokHareketTipi HareketTipi { get; set; }
        public List<Stock> Stocks { get; set; }=new List<Stock>();
   }
}
