using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;

public sealed class RemoveAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveAboutCommand, Result>
{
    public async Task<Result> Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (about is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimliğine sahip kayıt bulunamadı.");

        repository.Remove(about);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}