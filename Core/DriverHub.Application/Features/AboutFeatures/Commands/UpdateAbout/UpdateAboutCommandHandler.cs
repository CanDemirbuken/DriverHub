using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;

public sealed class UpdateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAboutCommand, Result>
{
    public async Task<Result> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (about is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimliğine sahip kayıt bulunamadı.");

        request.ApplyTo(about);

        repository.Update(about);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}