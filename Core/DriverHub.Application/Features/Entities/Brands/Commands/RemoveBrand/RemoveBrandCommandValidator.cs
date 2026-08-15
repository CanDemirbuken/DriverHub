using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Brands.Commands.RemoveBrand;

public sealed class RemoveBrandCommandValidator : AbstractValidator<RemoveBrandCommand>
{
    public RemoveBrandCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}