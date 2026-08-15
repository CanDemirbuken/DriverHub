using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Locations.Commands.RemoveLocation;

public sealed class RemoveLocationCommandValidator : AbstractValidator<RemoveLocationCommand>
{
    public RemoveLocationCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Silinecek lokasyonun Id bilgisi boş bırakılamaz.");
    }
}