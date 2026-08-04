using FluentValidation;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.AssignRoleToUser;

public sealed class AssignRoleToUserCommandValidator : AbstractValidator<AssignRoleToUserCommand>
{
    public AssignRoleToUserCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("Kullanıcı kimlik bilgisi boş olamaz.")
            .MaximumLength(450).WithMessage("Kullanıcı kimlik bilgisi 450 karakterden uzun olamaz.");

        RuleFor(command => command.RoleId)
            .NotEmpty().WithMessage("Rol kimlik bilgisi boş olamaz.")
            .MaximumLength(450).WithMessage("Rol kimlik bilgisi 450 karakterden uzun olamaz.");
    }
}