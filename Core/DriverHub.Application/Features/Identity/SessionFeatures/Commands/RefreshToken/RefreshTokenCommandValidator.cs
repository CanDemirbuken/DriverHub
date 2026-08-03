using FluentValidation;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token bilgisi boş olamaz.");
    }
}