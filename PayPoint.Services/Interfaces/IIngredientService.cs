using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IIngredientService
{
    Task<Ingredient?> GetIngredientByIdAsync(int IngredientId);
    Task<IEnumerable<Ingredient>> GetIngredientsAsync();
    Task<Ingredient?> AddIngredientAsync(IngredientCreateDto IngredientCreateDto);
    Task<bool> UpdateIngredientAsync(int id, IngredientUpdateDto IngredientUpdateDto);
    Task<bool> DeleteIngredientAsync(int id);
}
