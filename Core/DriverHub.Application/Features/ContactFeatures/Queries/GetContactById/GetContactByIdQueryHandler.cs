using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed class GetContactByIdQueryHandler(IRepository<Contact> repository, IMapper mapper) : IRequestHandler<GetContactByIdQuery, Result<GetContactByIdQueryResponse>>
{
    public async Task<Result<GetContactByIdQueryResponse>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (contact is null)
            return Result<GetContactByIdQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        GetContactByIdQueryResponse data = mapper.Map<GetContactByIdQueryResponse>(contact);
        return Result<GetContactByIdQueryResponse>.Success(data, StatusCodes.Status200OK);
    }
}