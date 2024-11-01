using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class SubCategoryController : BaseController
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoryController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubCategories()
    {
        IEnumerable<SubCategory> subCategories = await _subCategoryService.GetSubCategoriesAsync();

        return Ok(subCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubCategoryById(int id)
    {
        SubCategory? subCategory = await _subCategoryService.GetSubCategoryByIdAsync(id);

        if (subCategory.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(subCategory);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubCategory([FromBody] SubCategoryCreateDto subCategoryCreateDto)
    {
        SubCategory? subCategory = await _subCategoryService.AddSubCategoryAsync(subCategoryCreateDto);

        if (subCategory.IsNullOrEmpty())
        {
            return BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return CreatedAtAction(nameof(GetSubCategoryById), new { id = subCategory!.SubCategoryId }, subCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] SubCategoryUpdateDto subCategoryUpdateDto)
    {
       bool isUpdated = await _subCategoryService.UpdateSubCategoryAsync(id, subCategoryUpdateDto);

        if (!isUpdated)
        {
            return BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
       bool isDeleted = await _subCategoryService.DeleteSubCategoryAsync(id);
    
       if (!isDeleted)
       {
           BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
       }

       return Ok();
    }
}
