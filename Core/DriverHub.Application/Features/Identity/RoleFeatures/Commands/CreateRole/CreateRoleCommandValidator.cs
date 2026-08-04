using FluentValidation;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Rol bilgisi boş olamaz.")
            .MaximumLength(256)
            .WithMessage("Rol bilgisi 256 karakterden uzun olamaz.");
    }
}
