using FluentValidation;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.RemoveRoleFromUser;

public sealed class RemoveRoleFromUserCommandValidator : AbstractValidator<RemoveRoleFromUserCommand>
{
    public RemoveRoleFromUserCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("Kullanıcı kimlik bilgisi boş olamaz.")
            .MaximumLength(450).WithMessage("Kullanıcı kimlik bilgisi 450 karakterden uzun olamaz.");

        RuleFor(command => command.RoleId)
            .NotEmpty().WithMessage("Rol kimlik bilgisi boş olamaz.")
            .MaximumLength(450).WithMessage("Rol kimlik bilgisi 450 karakterden uzun olamaz.");
    }
}