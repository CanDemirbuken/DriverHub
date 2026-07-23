using DriverHub.Application.Features.BrandFeatures.Commands.CreateBrand;
using DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand;
using DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.BrandFeatures.Mappings;

public static class BrandMappingExtensions
{
    public static GetBrandByIdQueryResponse ToGetByIdResponse(
        this Brand brand)
    {
        return new GetBrandByIdQueryResponse(
            brand.Id,
            brand.Name);
    }

    public static Brand ToEntity(this CreateBrandCommand command)
    {
        return new Brand
        {
            Name = command.Name
        };
    }

    public static void ApplyTo(
        this UpdateBrandCommand command,
        Brand brand)
    {
        brand.Name = command.Name;
    }
}