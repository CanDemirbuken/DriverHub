using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;

public sealed class RemoveAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveAboutCommand>
{
    public async Task Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id);
        if (about is null)
            throw new NotFoundException();

        repository.Remove(about);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}