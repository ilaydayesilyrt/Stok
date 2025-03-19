using App.Repositories.Entities;
using App.Repositories.Interfaces;
using App.Services.StokHareketleri.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace App.Services.StokHareketleri
{
    public class StokHareketService(IMapper mapper,IUnitOfWork unitOfWork) : IStokHareketService
    {
        public async Task<ServiceResult> DeleteStockMovementAsync(Guid stockHarekettId)
        {
            var stockHareketIsExist = await unitOfWork.StokHareketleriRepository
                .Where(x => x.Id == stockHarekettId)
                .FirstOrDefaultAsync();
            if (stockHareketIsExist == null)
            {
                return ServiceResult.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            await unitOfWork.BeginTransactionAsync();
            try
            {
                await unitOfWork.StokHareketleriRepository.DeleteStokHareketAsync(stockHarekettId);
                await unitOfWork.SaveChangesAsync();
                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult<StokHareketDto>> GetStockMovementByIdAsync(Guid stockHarekettId)
        {
            var stockHareketIsExist = await unitOfWork.StokHareketleriRepository
                .Where(x => x.Id == stockHarekettId)
                .FirstOrDefaultAsync();
            if (stockHareketIsExist == null)
            {
                return ServiceResult<StokHareketDto>.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            var stockDto = mapper.Map<StokHareketDto>(stockHareketIsExist);
            return ServiceResult<StokHareketDto>.Success(stockDto, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<List<StokHareketDto>>> GetStockMovementsAsync()
        {
            var stockHareketIsExist = await unitOfWork.StokHareketleriRepository.GetAllAsync();
            if (stockHareketIsExist == null)
            {
                return ServiceResult<List<StokHareketDto>>.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            var stockDto = mapper.Map<List<StokHareketDto>>(stockHareketIsExist);
            return ServiceResult<List<StokHareketDto>>.Success(stockDto, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<int>> GetToplamStokMiktarı(string malKodu)
        {
            var stock = await unitOfWork.StockRepository
                .Where(x => x.MalKodu == malKodu)
                .FirstOrDefaultAsync();
            if (stock == null)
            {
                return ServiceResult<int>.Fail("Tanımlı stok bulunamadı", HttpStatusCode.NotFound);
            }
            var toplamGiris = await unitOfWork.StokHareketleriRepository.SumAsync<Stock>(
                x => x.MalKodu == malKodu && x.StokHareket.HareketTipi == Repositories.Enums.StokHareketTipi.StokIn,
                x => x.StokHareket.Miktar);
            var toplamCikis = await unitOfWork.StokHareketleriRepository.SumAsync<Stock>(
                x => x.MalKodu == malKodu && x.StokHareket.HareketTipi == Repositories.Enums.StokHareketTipi.StokOut,
                x => x.StokHareket.Miktar);
            var total = toplamGiris - toplamCikis;
            return ServiceResult<int>.Success(total, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<UpdateStokHareketRequest>> UpdateStokHareketAsync(UpdateStokHareketRequest request)
        {
            var stockHareketIsExist = await unitOfWork.StokHareketleriRepository.GetByIdAsync(request.Id);
            if (stockHareketIsExist == null)
            {
                return ServiceResult<UpdateStokHareketRequest>.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            await unitOfWork.BeginTransactionAsync();
            try
            {
                mapper.Map(request, stockHareketIsExist);
                await unitOfWork.StokHareketleriRepository.UpdateStokHareketAsync(stockHareketIsExist);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync();
                return ServiceResult<UpdateStokHareketRequest>.Success(request, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult<UpdateStokHareketRequest>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult<CreateStokHareketResponse>> CreateStokHareketAsync(CreateStokHareketRequest request)
        {
            await unitOfWork.BeginTransactionAsync();
            try
            {
                var stock = mapper.Map<StokHareket>(request);
                await unitOfWork.StokHareketleriRepository.AddStokHareketAsync(stock);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync(); 
                return ServiceResult<CreateStokHareketResponse>.SuccessAsCreated(new CreateStokHareketResponse(stock.Id), $"api/StokHareket/{stock.Id}");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult<CreateStokHareketResponse>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}


