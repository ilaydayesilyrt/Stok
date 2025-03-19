namespace App.Repositories.Entities
{
    public class Stock : BaseEntity<Guid>
    {
        public string MalKodu { get; set; } = default!;
        public string MalAdi {  get; set; } = default!;
        public string Birim { get; set; } = default!;
        public Guid StokHareketId { get; set; }
        public StokHareket StokHareket { get; set; }
    }
}
