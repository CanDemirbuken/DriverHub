using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;

public sealed class RemoveAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveAboutCommand>
{
    public async Task Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id);

        repository.Remove(about);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}