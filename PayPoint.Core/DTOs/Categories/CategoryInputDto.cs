namespace PayPoint.Core.DTOs.Categories;

public class CategoryInputDto
{
    public CategoryInputDto(bool IncludeSubCategories)
    {
        this.IncludeSubCategories = IncludeSubCategories;
    }

    public bool IncludeSubCategories { get; set; }
}
