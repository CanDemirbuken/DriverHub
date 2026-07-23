using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.ContactFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.UpdateContact;

public sealed class UpdateContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateContactCommand>
{
    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            throw new NotFoundException();

        request.ApplyTo(contact);

        repository.Update(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}