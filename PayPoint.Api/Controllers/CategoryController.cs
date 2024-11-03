using Microsoft.AspNetCore.Mvc;
using PayPoint.Api.Extensions;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromHeader(Name = "IncludeSubCategories")] bool? includeSubCategories)
    {
        CategoryInputDto categoryDto = new CategoryInputDto(IncludeSubCategories: includeSubCategories == true);
        IEnumerable<Category> categories = await _categoryService.GetCategoriesAsync(categoryDto);

        categories = categories.Select(x => x.ToCategoryHasIncludes(categoryDto.IncludeSubCategories));

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id, [FromHeader(Name = "IncludeSubCategories")] bool? includeSubCategories)
    {
        CategoryInputDto categoryDto = new CategoryInputDto(IncludeSubCategories: includeSubCategories == true);
        Category? category = await _categoryService.GetCategoryByIdAsync(id, categoryDto);

        if (category.IsNullOrEmpty())
        {
            return NotFound("Categoria no encontrada.");
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
            BadRequest(ErrorMessageBadRequest);
        }
        
        category = category!.ToCategoryHasIncludes(IncludeSubCategories: false);
        
        return CreatedAtAction(nameof(GetCategoryById), new { id = category!.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        bool isUpdated = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);

        if (!isUpdated)
        {
            BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        bool isDeleted = await _categoryService.DeleteCategoryAsync(id);

        if (!isDeleted)
        {
            BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }
}
