using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ISubCategoryService
{
    Task<SubCategory?> GetSubCategoryByIdAsync(int SubCategoryId);
    Task<IEnumerable<SubCategory>> GetSubCategoriesAsync();
    Task<SubCategory?> AddSubCategoryAsync(SubCategoryCreateDto SubCategoryCreateDto);
    Task<bool> UpdateSubCategoryAsync(int id, SubCategoryUpdateDto SubCategoryUpdateDto);
    Task<bool> DeleteSubCategoryAsync(int id);
}
