using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;

public sealed class UpdateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAboutCommand>
{
    public async Task Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id);

        if (about is null)
            throw new NotFoundException();

        request.ApplyTo(about);

        repository.Update(about);
        await unitOfWork.SaveChangesAsync();
    }
}