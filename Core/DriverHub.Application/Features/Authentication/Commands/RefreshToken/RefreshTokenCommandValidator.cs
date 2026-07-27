using DriverHub.Application.Features.Authentication.Commands.RefreshToken;
using FluentValidation;

namespace DriverHub.Application.Features.AuthenticationFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token bilgisi boş olamaz.");
    }
}