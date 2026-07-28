using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed class GetContactByIdQueryHandler(IRepository<Contact> repository, IMapper mapper) : IRequestHandler<GetContactByIdQuery, Result<GetContactByIdQueryResponse>>
{
    public async Task<Result<GetContactByIdQueryResponse>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            return Result<GetContactByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        GetContactByIdQueryResponse data = mapper.Map<GetContactByIdQueryResponse>(contact);
        return Result<GetContactByIdQueryResponse>.Success(data);
    }
}