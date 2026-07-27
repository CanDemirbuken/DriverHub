using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed class GetAboutByIdQueryHandler(IRepository<About> repository, IMapper mapper) : IRequestHandler<GetAboutByIdQuery, Result<GetAboutByIdQueryResponse>>
{
    public async Task<Result<GetAboutByIdQueryResponse>> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (about is null)
            return Result<GetAboutByIdQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimliğine sahip kayıt bulunamadı.");

        var data = mapper.Map<GetAboutByIdQueryResponse>(about);
        return Result<GetAboutByIdQueryResponse>.Success(data, StatusCodes.Status200OK);
    }
}