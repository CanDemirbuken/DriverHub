using FluentValidation;

namespace DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;

public sealed class UpdateAboutCommandValidator: AbstractValidator<UpdateAboutCommand>
{
    public UpdateAboutCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Başlık alanı boş bırakılamaz.")
            .MaximumLength(150)
            .WithMessage("Başlık alanı en fazla 150 karakter olabilir.");

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Açıklama alanı boş bırakılamaz.")
            .MaximumLength(2000)
            .WithMessage("Açıklama alanı en fazla 2000 karakter olabilir.");

        RuleFor(command => command.ImageUrl)
            .NotEmpty()
            .WithMessage("Görsel URL alanı boş bırakılamaz.")
            .MaximumLength(500)
            .WithMessage("Görsel URL alanı en fazla 500 karakter olabilir.");
    }
}