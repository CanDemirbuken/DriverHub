using FluentValidation;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.RemoveRole;

public sealed class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
{
    public RemoveRoleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Rol kimlik bilgisi boş olamaz.");
    }
}