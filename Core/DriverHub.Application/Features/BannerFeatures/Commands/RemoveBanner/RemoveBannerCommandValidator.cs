using FluentValidation;

namespace DriverHub.Application.Features.BannerFeatures.Commands.RemoveBanner;

public sealed class RemoveBannerCommandValidator : AbstractValidator<RemoveBannerCommand>
{
    public RemoveBannerCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}
