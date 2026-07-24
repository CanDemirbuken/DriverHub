using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;

public sealed class UpdateBannerCommandValidator : AbstractValidator<UpdateBannerCommand>
{
    public UpdateBannerCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Başlık alanı boş bırakılamaz.")
            .MaximumLength(150)
            .WithMessage("Başlık alanı en fazla 150 karakter olabilir.");

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Açıklama alanı boş bırakılamaz.")
            .MaximumLength(1000)
            .WithMessage("Açıklama alanı en fazla 1000 karakter olabilir.");

        RuleFor(command => command.VideoDescription)
            .NotEmpty()
            .WithMessage("Video Açıklama alanı boş bırakılamaz.")
            .MaximumLength(1000)
            .WithMessage("Video Açıklama alanı en fazla 1000 karakter olabilir.");

        RuleFor(command => command.VideoUrl)
            .NotEmpty()
            .WithMessage("Video Url alanı boş bırakılamaz.")
            .MaximumLength(500)
            .WithMessage("Video Url alanı en fazla 500 karakter olabilir.");
    }
}