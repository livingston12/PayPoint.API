using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ISubCategoryService
{
    Task<SubCategory?> GetSubCategoryByIdAsync(int SubCategoryId);
    Task<IEnumerable<SubCategory>> GetSubCategoriesAsync();
    Task AddSubCategoryAsync(SubCategoryCreateDto SubCategoryCreateDto);
    Task UpdateSubCategoryAsync(int id, SubCategoryUpdateDto SubCategoryUpdateDto);
    Task DeleteSubCategoryAsync(int id);
}
