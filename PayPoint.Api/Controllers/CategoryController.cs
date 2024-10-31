using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    private readonly ISubCategoryService _subCategoryService;

    public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
    {
        _categoryService = categoryService;
        _subCategoryService = subCategoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetCategories([FromHeader] CategoryDto categoryDto)
    {
        return await _categoryService.GetCategoriesAsync(categoryDto);
    }

    [HttpGet("{id}")]
    public async Task<Category?> GetCategoryById(int id, [FromHeader] CategoryDto categoryDto)
    {
        return await _categoryService.GetCategoryByIdAsync(id, categoryDto);
    }

    [HttpPost]
    public async Task AddCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        await _categoryService.AddCategoryAsync(categoryCreateDto);
    }

    [HttpPut("{id}")]
    public async Task UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
    }

    [HttpGet("subcategories")]
    public async Task<IEnumerable<SubCategory>> GetSubCategories()
    {
        return await _subCategoryService.GetSubCategoriesAsync();
    }

    [HttpGet("subcategories/{subCategoryId}")]
    public async Task<SubCategory?> GetSubCategoryById(int subCategoryId)
    {
        return await _subCategoryService.GetSubCategoryByIdAsync(subCategoryId);
    }

    [HttpPost("subcategories")]
    public async Task AddSubCategory([FromBody] SubCategoryCreateDto subCategoryCreateDto)
    {
        await _subCategoryService.AddSubCategoryAsync(subCategoryCreateDto);
    }

    [HttpPut("subcategories/{id}")]
    public async Task UpdateSubCategory(int id, [FromBody] SubCategoryUpdateDto subCategoryUpdateDto)
    {
        await _subCategoryService.UpdateSubCategoryAsync(id, subCategoryUpdateDto);
    }

    [HttpDelete("subcategories/{id}")]
    public async Task DeleteSubCategory(int id)
    {
        await _subCategoryService.DeleteSubCategoryAsync(id);
    }
}
