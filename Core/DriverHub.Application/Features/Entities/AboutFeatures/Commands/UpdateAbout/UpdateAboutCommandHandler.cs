using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.AboutFeatures.Commands.UpdateAbout;

public sealed class UpdateAboutCommandHandler(IRepository<About> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAboutCommand, Result>
{
    public async Task<Result> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (about is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimliğine sahip kayıt bulunamadı.", nameof(request.Id)));

        mapper.Map(request, about);

        repository.Update(about);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}