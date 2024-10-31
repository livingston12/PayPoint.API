using Microsoft.AspNetCore.Mvc;
using PayPoint.Api.Extensions;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Extensions;
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
    public async Task<IActionResult> GetCategories([FromHeader(Name = "IncludeSubCategories")] bool? includeSubCategories)
    {
        CategoryDto categoryDto = new CategoryDto(IncludeSubCategories: includeSubCategories == true);
        IEnumerable<Category> categories = await _categoryService.GetCategoriesAsync(categoryDto);

        categories = categories.Select(x => x.ToCategoryHasIncludes(categoryDto.IncludeSubCategories));

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id, [FromHeader(Name = "IncludeSubCategories")] bool? includeSubCategories)
    {
        CategoryDto categoryDto = new CategoryDto(IncludeSubCategories: includeSubCategories == true);
        Category? category = await _categoryService.GetCategoryByIdAsync(id, categoryDto);

        if (category.IsNullOrEmpty())
        {
            return NotFound("Category not found.");
        }

        category = category!.ToCategoryHasIncludes(categoryDto.IncludeSubCategories);

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        Category? category = await _categoryService.AddCategoryAsync(categoryCreateDto);

        if (category.IsNullOrEmpty())
        {
            BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }
        
        category = category!.ToCategoryHasIncludes(IncludeSubCategories: false);
        
        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        int rowsUpdated = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);

        if (rowsUpdated == 0)
        {
            BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        int rowsDeleted = await _categoryService.DeleteCategoryAsync(id);

        if (rowsDeleted == 0)
        {
            BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return Ok();
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
