using DriverHub.Application.Common.Validations;
using DriverHub.Domain.Enums;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarPricings;

public sealed class SetCarPricingsCommandValidator : AbstractValidator<SetCarPricingsCommand>
{
    public SetCarPricingsCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Fiyatları güncellenecek aracın Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Pricings)
            .NotEmpty()
            .WithMessage("Araç fiyat bilgileri boş bırakılamaz.")
            .Must(pricings => pricings.Count == Enum.GetValues<PricingType>().Length)
            .WithMessage("Daily, Weekly ve Monthly fiyatlarının tamamı gönderilmelidir.")
            .Must(pricings => pricings.Select(x => x.Type).Distinct().Count() == pricings.Count)
            .WithMessage("Aynı fiyat tipi birden fazla kez gönderilemez.");

        RuleForEach(x => x.Pricings)
            .ChildRules(pricing =>
            {
                pricing.RuleFor(x => x.Type)
                    .IsInEnum()
                    .WithMessage("Geçerli bir fiyat tipi seçilmelidir.")
                    .Must(type => type != 0)
                    .WithMessage("Fiyat tipi boş bırakılamaz.");

                pricing.RuleFor(x => x.Amount)
                    .GreaterThan(0)
                    .WithMessage("Fiyat 0'dan büyük olmalıdır.");
            });
    }
}