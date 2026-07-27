using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand;

public sealed class UpdateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBrandCommand, Result>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        bool brandExists = await repository.AnyAsync(predicate: b => b.Id != request.Id && b.Name == request.Name, cancellationToken);
        if (brandExists)
            return Result.Failure(StatusCodes.Status409Conflict, "Bu isimde başka bir marka zaten mevcut");

        mapper.Map(request, brand);

        repository.Update(brand);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}