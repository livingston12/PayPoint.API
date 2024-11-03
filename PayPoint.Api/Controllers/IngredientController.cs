using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class IngredientController : BaseController
{
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetIngredients()
    {
        IEnumerable<Ingredient> ingredients = await _ingredientService.GetIngredientsAsync();

        return Ok(ingredients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredientById(int id)
    {
        Ingredient? ingredient = await _ingredientService.GetIngredientByIdAsync(id);

        if (ingredient.IsNullOrEmpty())
        {
            return NotFound("Ingrediente no encontrado.");
        }

        return Ok(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] IngredientCreateDto ingredientCreateDto)
    {
        Ingredient? ingredient = await _ingredientService.AddIngredientAsync(ingredientCreateDto);

        if (ingredient.IsNullOrEmpty())
        {
            return BadRequest("Error Inesperado: intente de nuevo o contacte con el administrador.");
        }

        return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient!.IngredientId }, ingredient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientUpdateDto ingredientUpdateDto)
    {
        bool isUpdated = await _ingredientService.UpdateIngredientAsync(id, ingredientUpdateDto);

        if (!isUpdated)
        {
            return BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        bool isDeleted = await _ingredientService.DeleteIngredientAsync(id);

        if (!isDeleted)
        {
            return BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }

}
