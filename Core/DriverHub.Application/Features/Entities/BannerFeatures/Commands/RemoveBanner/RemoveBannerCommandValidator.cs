using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Commands.RemoveBanner;

public sealed class RemoveBannerCommandValidator : AbstractValidator<RemoveBannerCommand>
{
    public RemoveBannerCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}
