using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces;
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

        about.Title = request.Title;
        about.Description = request.Description;
        about.ImageUrl = request.ImageUrl;

        repository.Update(about);
        await unitOfWork.SaveChangesAsync();
    }
}