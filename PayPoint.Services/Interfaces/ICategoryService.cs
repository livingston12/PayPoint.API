using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(int CategoryId, CategoryInputDto CategoryDto);
    Task<IEnumerable<Category>> GetCategoriesAsync(CategoryInputDto CategoryDto);
    Task<Category?> AddCategoryAsync(CategoryCreateDto CategoryCreateDto);
    Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto CategoryUpdateDto);
    Task<bool> DeleteCategoryAsync(int id);
}
