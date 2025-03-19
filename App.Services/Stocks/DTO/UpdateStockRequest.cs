namespace App.Services.Stocks.DTO
{
    public record UpdateStockRequest(Guid Id, string MalKodu, string MalAdi, string Birim, Guid StokHareketId);
}
