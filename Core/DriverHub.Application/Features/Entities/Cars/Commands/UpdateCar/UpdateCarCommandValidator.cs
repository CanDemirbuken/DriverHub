using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Commands.UpdateCar;

public sealed class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
{
    public UpdateCarCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.BrandId)
            .NotEmpty()
            .WithMessage("Brand bilgisi zorunludur.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Kategori bilgisi zorunludur.");

        RuleFor(x => x.CurrentLocationId)
            .NotEmpty()
            .WithMessage("Lokasyon bilgisi zorunludur.");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("Model bilgisi zorunludur.")
            .MaximumLength(150)
            .WithMessage("Model bilgisi en fazla 150 karakter olabilir.");

        RuleFor(x => x.ModelYear)
            .InclusiveBetween((short)1900, (short)2100)
            .WithMessage("Model yılı 1900 ile 2100 arasında olmalıdır.");

        RuleFor(x => x.Plate)
            .NotEmpty()
            .WithMessage("Plaka bilgisi zorunludur.")
            .MaximumLength(20)
            .WithMessage("Plaka en fazla 20 karakter olabilir.");

        RuleFor(x => x.Vin)
            .NotEmpty()
            .WithMessage("VIN bilgisi zorunludur.")
            .Length(17)
            .WithMessage("VIN 17 karakter olmalıdır.");

        RuleFor(x => x.CoverImageUrl)
            .NotEmpty()
            .WithMessage("Kapak görseli zorunludur.")
            .MaximumLength(500)
            .WithMessage("Kapak görseli en fazla 500 karakter olabilir.");

        RuleFor(x => x.Km)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilometre 0 veya daha büyük olmalıdır.");

        RuleFor(x => x.Transmission)
            .NotEmpty()
            .WithMessage("Şanzıman bilgisi zorunludur.")
            .MaximumLength(50)
            .WithMessage("Şanzıman bilgisi en fazla 50 karakter olabilir.");

        RuleFor(x => x.Seat)
            .InclusiveBetween((byte)1, (byte)9)
            .WithMessage("Koltuk sayısı 1 ile 9 arasında olmalıdır.");

        RuleFor(x => x.Luggage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Bagaj kapasitesi 0 veya daha büyük olmalıdır.");

        RuleFor(x => x.Fuel)
            .NotEmpty()
            .WithMessage("Yakıt tipi zorunludur.")
            .MaximumLength(50)
            .WithMessage("Yakıt tipi en fazla 50 karakter olabilir.");

        RuleFor(x => x.Color)
            .NotEmpty()
            .WithMessage("Renk bilgisi zorunludur.")
            .MaximumLength(50)
            .WithMessage("Renk bilgisi en fazla 50 karakter olabilir.");

        RuleFor(x => x.BigImageUrl)
            .NotEmpty()
            .WithMessage("Büyük görsel zorunludur.")
            .MaximumLength(500)
            .WithMessage("Büyük görsel en fazla 500 karakter olabilir.");
    }
}