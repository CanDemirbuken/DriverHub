using AutoMapper;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.CreateCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.UpdateCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Queries.GetCategoryById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetCategoryByIdQueryResponse>();

        CreateMap<CreateCategoryCommand, Category>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateCategoryCommand, Category>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}