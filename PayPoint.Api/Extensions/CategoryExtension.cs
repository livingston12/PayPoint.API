using PayPoint.Core.Models;

namespace PayPoint.Api.Extensions;

public static class CategoryExtension
{
    public static Category ToCategoryHasIncludes(this Category category, bool IncludeSubCategories)
    {
        return new Category
        {
            CategoryId = category!.CategoryId,
            Name = category.Name,
            Description = category.Description,
            Status = category.Status,
            SubCategories = IncludeSubCategories ? category.SubCategories : null
        };
    }
}
