using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;

public sealed class CreateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAboutCommand>
{
    public async Task Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        About about = new()
        {
            Title = request.Title,
            Description = request.Description,
            ImageUrl = request.ImageUrl
        };

        await repository.AddAsync(about, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}