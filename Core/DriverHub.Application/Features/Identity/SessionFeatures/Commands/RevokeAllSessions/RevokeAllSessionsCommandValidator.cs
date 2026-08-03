using FluentValidation;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeAllSessions;

public sealed class RevokeAllSessionsCommandValidator : AbstractValidator<RevokeAllSessionsCommand>
{
    public RevokeAllSessionsCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithMessage("Kullanıcı ID bilgisi boş olamaz.");
    }
}