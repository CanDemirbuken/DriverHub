using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.AboutFeatures.Commands.CreateAbout;

public sealed class CreateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAboutCommand, Result<CreateAboutCommandResponse>>
{
    public async Task<Result<CreateAboutCommandResponse>> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        About about = mapper.Map<About>(request);

        await repository.AddAsync(about, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateAboutCommandResponse data = new CreateAboutCommandResponse(about.Id);
        return Result<CreateAboutCommandResponse>.Success(data);
    }
}