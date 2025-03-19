using App.Services.StokHareketleri.DTO;
using FluentValidation;
namespace App.Services.StokHareketleri.Validators
{
    public class UpdateStokHareketRequestValidator : AbstractValidator<UpdateStokHareketRequest>
    {
        public UpdateStokHareketRequestValidator()
        {
            RuleFor(x => x.StokHareketTipi)

                .NotEmpty().WithMessage("hareket tipi gereklidir.");

            RuleFor(x => x.Miktar)
               .NotEmpty().WithMessage("miktar gereklidir.");
        }
    }
}
