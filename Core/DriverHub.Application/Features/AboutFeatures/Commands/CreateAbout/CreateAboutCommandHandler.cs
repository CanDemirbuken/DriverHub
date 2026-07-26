using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;

public sealed class CreateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAboutCommand, Result<CreateAboutCommandResponse>>
{
    public async Task<Result<CreateAboutCommandResponse>> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        About about = request.ToEntity();

        await repository.AddAsync(about, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateAboutCommandResponse data = new CreateAboutCommandResponse(about.Id);
        return Result<CreateAboutCommandResponse>.Success(data, StatusCodes.Status201Created);
    }
}