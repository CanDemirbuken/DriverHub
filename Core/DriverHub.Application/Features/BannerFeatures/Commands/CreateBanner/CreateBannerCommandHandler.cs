using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;

public sealed class CreateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBannerCommand>
{
    public async Task Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        Banner banner = new Banner()
        {
            Title = request.Title,
            Description = request.Description,
            VideoDescription = request.VideoDescription,
            VideoUrl = request.VideoUrl
        };

        await repository.AddAsync(banner, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}