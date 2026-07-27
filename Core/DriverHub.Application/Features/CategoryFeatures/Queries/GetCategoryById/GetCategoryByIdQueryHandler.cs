using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(IRepository<Category> repository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdQueryResponse>>
{
    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            return Result<GetCategoryByIdQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        GetCategoryByIdQueryResponse data = mapper.Map<GetCategoryByIdQueryResponse>(category);
        return Result<GetCategoryByIdQueryResponse>.Success(data, StatusCodes.Status200OK);
    }
}