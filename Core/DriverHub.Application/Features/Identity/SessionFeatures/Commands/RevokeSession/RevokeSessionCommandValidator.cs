using FluentValidation;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeSession;

public sealed class RevokeSessionCommandValidator : AbstractValidator<RevokeSessionCommand>
{
    public RevokeSessionCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
    .NotEmpty()
    .WithMessage("Refresh token bilgisi boş olamaz.");
    }
}