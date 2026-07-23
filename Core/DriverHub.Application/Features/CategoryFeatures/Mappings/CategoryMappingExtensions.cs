using DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;
using DriverHub.Application.Features.CategoryFeatures.Commands.UpdateCategory;
using DriverHub.Application.Features.CategoryFeatures.Queries.GetCategoryById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.CategoryFeatures.Mappings;

public static class CategoryMappingExtensions
{
    public static GetCategoryByIdQueryResponse ToGetByIdResponse(
        this Category category)
    {
        return new GetCategoryByIdQueryResponse(
            category.Id,
            category.Name);
    }

    public static Category ToEntity(this CreateCategoryCommand command)
    {
        return new Category
        {
            Name = command.Name
        };
    }

    public static void ApplyTo(
        this UpdateCategoryCommand command,
        Category category)
    {
        category.Name = command.Name;
    }
}