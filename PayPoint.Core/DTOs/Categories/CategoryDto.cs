namespace PayPoint.Core.DTOs.Categories;

public class CategoryDto
{
    public CategoryDto(bool IncludeSubCategories)
    {
        this.IncludeSubCategories = IncludeSubCategories;
    }

    public bool IncludeSubCategories { get; set; }
}
