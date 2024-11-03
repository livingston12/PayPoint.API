using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Entities;
using PayPoint.Core.Enums;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;

namespace PayPoint.Services.Implementations;

public class BaseService
{
    private readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected async Task AddIngredientsToProduct(ProductEntity productEntity, IEnumerable<int> ingredientIds)
    {
        foreach (int ingredientId in ingredientIds)
        {
            await AddIngredientToProduct(productEntity, ingredientId);
        }
    }

    protected async Task AddIngredientsToProduct(ProductEntity productEntity, Dictionary<int, int?> ingredientQuantityIds)
    {
        foreach (KeyValuePair<int, int?> ingredientQuantity in ingredientQuantityIds)
        {
            int ingredientId = ingredientQuantity.Key;
            int quantity = ingredientQuantity.Value ?? 1;

            await AddIngredientToProduct(productEntity, ingredientId, quantity);
        }
    }

    protected async Task AddIngredientToProduct(ProductEntity productEntity, int ingredientId, int quantity = 0)
    {
        IngredientEntity? ingredientEntity = await GetIngredientById(ingredientId, false);

        if (ingredientEntity.IsNotNullOrEmpty())
        {
            productEntity.ProductIngredients!.Add(new ProductIngredientEntity
            {
                ProductId = productEntity.ProductId,
                IngredientId = ingredientId,
                Ingredient = ingredientEntity!,
                Quantity = quantity
            });
        }
    }

    protected async void UpdateRemoveIngredientsToProduct(ProductEntity productEntity, IEnumerable<int> ingredientIdsToUpdate)
    {
        List<int> existingIngredientIds = productEntity.ProductIngredients!.Select(pi => pi.IngredientId).ToSafeList();

        // Delete ingredients that are not in the new list.
        foreach (int ingredientId in existingIngredientIds)
        {
            if (!ingredientIdsToUpdate.Contains(ingredientId))
            {
                ProductIngredientEntity ingredientToRemove = productEntity.ProductIngredients!.First(pi => pi.IngredientId == ingredientId);
                productEntity.ProductIngredients!.Remove(ingredientToRemove);
            }
        }

        // Add new ingredients.
        await AddIngredientsToProduct(productEntity, ingredientIdsToUpdate);
    }

    protected async Task UpdateIngredientsToProduct(ProductEntity productEntity, Dictionary<int, int?> ingredientIdsToUpdate)
    {
        List<int> existingIngredientIds = productEntity.ProductIngredients!.Select(pi => pi.IngredientId).ToSafeList();
        Dictionary<int, int?> ingredientIdsToInsert = ingredientIdsToUpdate;

        // update quantities of existing ingredients.
        foreach (int ingredientId in existingIngredientIds)
        {
            if (ingredientIdsToUpdate.TryGetValue(ingredientId, out int? quantity))
            {
                ProductIngredientEntity ingredientToUpdate = productEntity.ProductIngredients!.First(pi => pi.IngredientId == ingredientId);
                ingredientToUpdate.Quantity = quantity ?? 1;

                // Remove from the list of ingredients to insert.
                ingredientIdsToInsert.Remove(ingredientId);
            }
        }

        // Add new ingredients.
        await AddIngredientsToProduct(productEntity, ingredientIdsToInsert);
    }

    protected async Task<SubCategoryEntity> GetSubCategoryById(int subCategoryId)
    {
        SubCategoryEntity? subCategoryEntity = await _unitOfWork.SubCategories.GetByIdAsync(subCategoryId);

        if (subCategoryEntity.IsNullOrEmpty())
        {
            throw new Exception("subcategory not found");
        }

        return subCategoryEntity!;
    }

    protected async Task<ProductEntity> GetProductById(int productId, bool includeProductIngredients = false)
    {
        ProductEntity? productEntity = null;

        if (includeProductIngredients)
        {
            productEntity = await _unitOfWork.Products.AsQueryable()
                .Include(p => p.ProductIngredients)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }
        else
        {
            productEntity = await _unitOfWork.Products.GetByIdAsync(productId);
        }

        if (productEntity.IsNullOrEmpty())
        {
            throw new Exception("Product not found");
        }

        return productEntity!;
    }

    protected async Task<CategoryEntity> GetCategoryById(int categoryId)
    {
        CategoryEntity? categoryEntity = await _unitOfWork.Categories.GetByIdAsync(categoryId);

        if (categoryEntity.IsNullOrEmpty())
        {
            throw new Exception("Category not found.");
        }

        return categoryEntity!;
    }

    protected async Task<IngredientEntity?> GetIngredientById(int ingredientId, bool throwIfNotFound = true)
    {
        IngredientEntity? ingredientEntity = await _unitOfWork.Ingredients.GetByIdAsync(ingredientId);

        if (ingredientEntity.IsNullOrEmpty() && throwIfNotFound)
        {
            throw new Exception("Ingredient not found.");
        }

        return ingredientEntity;
    }
}
