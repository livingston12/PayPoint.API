using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(int CategoryId, CategoryDto CategoryDto);
    Task<IEnumerable<Category>> GetCategoriesAsync(CategoryDto CategoryDto);
    Task AddCategoryAsync(CategoryCreateDto CategoryCreateDto);
    Task UpdateCategoryAsync(int id, CategoryUpdateDto CategoryUpdateDto);
    Task DeleteCategoryAsync(int id);
}
