namespace App.Services.Stocks.DTO
{
    public record CreateStockRequest(string MalKodu, string MalAdi, string Birim, Guid StokHareketId);
}
