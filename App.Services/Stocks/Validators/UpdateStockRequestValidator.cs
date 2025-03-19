using App.Services.Stocks.DTO;
using FluentValidation;
namespace App.Services.Stocks.Validators
{
    public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>
    {
        public UpdateStockRequestValidator()
        {

            RuleFor(x => x.MalKodu)

                .NotEmpty().WithMessage("mal kodu gereklidir.")
                .Length(1, 10).WithMessage("mal kodu 1-10 karakterden oluşmalıdır");

            RuleFor(x => x.MalAdi)
               .NotEmpty().WithMessage("mal adı gereklidir.");
        }
    }
}
