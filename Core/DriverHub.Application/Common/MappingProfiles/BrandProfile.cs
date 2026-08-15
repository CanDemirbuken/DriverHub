using AutoMapper;
using DriverHub.Application.Features.Entities.Brands.Commands.CreateBrand;
using DriverHub.Application.Features.Entities.Brands.Commands.UpdateBrand;
using DriverHub.Application.Features.Entities.Brands.Queries.GetAllBrand;
using DriverHub.Application.Features.Entities.Brands.Queries.GetBrandById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, GetAllBrandQueryResponse>();
        CreateMap<Brand, GetBrandByIdQueryResponse>();

        CreateMap<CreateBrandCommand, Brand>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Cars, options => options.Ignore());

        CreateMap<UpdateBrandCommand, Brand>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Cars, options => options.Ignore());
    }
}