using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.ContactFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed class GetContactByIdQueryHandler(IRepository<Contact> repository) : IRequestHandler<GetContactByIdQuery, GetContactByIdQueryResponse>
{
    public async Task<GetContactByIdQueryResponse> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            throw new NotFoundException();

        GetContactByIdQueryResponse response = contact.ToGetByIdResponse();
        return response;
    }
}