using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(int CategoryId, CategoryDto CategoryDto);
    Task<IEnumerable<Category>> GetCategoriesAsync(CategoryDto CategoryDto);
    Task<Category?> AddCategoryAsync(CategoryCreateDto CategoryCreateDto);
    Task<int> UpdateCategoryAsync(int id, CategoryUpdateDto CategoryUpdateDto);
    Task<int> DeleteCategoryAsync(int id);
}
