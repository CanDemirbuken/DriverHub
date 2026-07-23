using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.RemoveContact;

public sealed class RemoveContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveContactCommand>
{
    public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            throw new NotFoundException();

        repository.Remove(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
