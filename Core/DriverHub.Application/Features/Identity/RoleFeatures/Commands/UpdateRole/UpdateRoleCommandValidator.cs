using FluentValidation;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.UpdateRole;

public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Rol kimlik bilgisi boş olamaz.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Rol bilgisi boş olamaz.")
            .MaximumLength(256)
            .WithMessage("Rol bilgisi 256 karakterden uzun olamaz.");
    }
}
