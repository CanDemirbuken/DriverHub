using DriverHub.Application.Features.ContactFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetAllContact;

public sealed class GetAllContactQueryHandler(IRepository<Contact> repository) : IRequestHandler<GetAllContactQuery, IReadOnlyList<GetAllContactQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllContactQueryResponse>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
    {
        var contacts = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllContactQueryResponse> response = contacts.Select(c => c.ToGetAllResponse()).ToList();
        return response;
    }
}
