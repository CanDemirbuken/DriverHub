using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.CarFeatures.Commands.RemoveCar;

public sealed class RemoveCarCommandValidator : AbstractValidator<RemoveCarCommand>
{
    public RemoveCarCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}