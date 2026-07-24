using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;

public sealed class CreateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAboutCommand, CreateAboutCommandResponse>
{
    public async Task<CreateAboutCommandResponse> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        About about = request.ToEntity();

        await repository.AddAsync(about, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateAboutCommandResponse response = new CreateAboutCommandResponse(about.Id);
        return response;
    }
}