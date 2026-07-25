using DriverHub.Application.Features.ContactFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.CreateContact;

public sealed class CreateContactCommandHandler(IRepository<Contact> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateContactCommand, CreateContactCommandResponse>
{
    public async Task<CreateContactCommandResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        Contact contact = request.ToEntity();

        await repository.AddAsync(contact, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateContactCommandResponse response = new CreateContactCommandResponse(contact.Id);
        return response;
    }
}