using App.Services.StokHareketleri.DTO;
using FluentValidation;
namespace App.Services.StokHareketleri.Validators
{
    public class CreateStokHareketRequestValidator : AbstractValidator<CreateStokHareketRequest>
    {
        public CreateStokHareketRequestValidator()
        {
            RuleFor(x => x.HareketTipi)
                .NotEmpty().WithMessage("hareket tipi gereklidir.");
            RuleFor(x => x.Miktar)
               .NotEmpty().WithMessage("miktar gereklidir.");
        }
    }
}
