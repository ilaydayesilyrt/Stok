using App.Repositories.Entities;
using App.Repositories.Interfaces;
using App.Services.Stocks.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace App.Services.Stocks
{
    public class StockService(IMapper mapper,IUnitOfWork unitOfWork) : IStockService
    {
        public async Task<ServiceResult<CreateStockResponse>> CreateStockAsync(CreateStockRequest request)
        {
            var stockIsExist = await unitOfWork.StockRepository.Where(x => x.MalAdi == request.MalAdi).AnyAsync();
            if (stockIsExist)
            {
                return ServiceResult<CreateStockResponse>.Fail("Ürün ismi veritabanında bulunmaktadir.", HttpStatusCode.BadRequest);
            }
            await unitOfWork.BeginTransactionAsync();
            try
            {
                var stock = mapper.Map<Stock>(request);
                await unitOfWork.StockRepository.AddStockAsync(stock);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync(); 
                return ServiceResult<CreateStockResponse>.SuccessAsCreated(new CreateStockResponse(stock.Id), $"api/stock/{stock.Id}");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult<CreateStockResponse>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult> DeleteStockAsync(Guid id)
        {
            var stockIsExist = await unitOfWork.StockRepository.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (stockIsExist is null)
            {
                return ServiceResult.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            await unitOfWork.BeginTransactionAsync();
            try
            {
                await unitOfWork.StockRepository.DeleteStockAsync(id);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync();
                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult> UpdateStockAsync(UpdateStockRequest request)
        {
            var stockIsExist = await unitOfWork.StockRepository.GetByIdAsync(request.Id);
            if (stockIsExist is null)
            {
                return ServiceResult.Fail("Stock not found", HttpStatusCode.NotFound);
            }
            await unitOfWork.BeginTransactionAsync();
            try
            {
                mapper.Map(request, stockIsExist);
                await unitOfWork.StockRepository.UpdateStockAsync(stockIsExist);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync(); 
                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return ServiceResult.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}

