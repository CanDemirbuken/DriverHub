using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Commands.RemoveContact;

public sealed class RemoveContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveContactCommand, Result>
{
    public async Task<Result> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        repository.Remove(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}