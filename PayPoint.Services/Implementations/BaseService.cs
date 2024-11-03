using System.Linq.Expressions;
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

    protected void AddIngredients(ProductEntity productEntity, IEnumerable<int> ingredientIds)
    {
        // productCreateDto.IngredientIds.ToSafeList()
        foreach (int ingredientId in ingredientIds)
        {
            productEntity.ProductIngredients!.Add(new ProductIngredientEntity
            {
                ProductId = productEntity.ProductId,
                IngredientId = ingredientId
            });
        }
    }

    protected void UpdateIngredients(ProductEntity productEntity, ProductUpdateDto productUpdateDto)
    {
        List<int>? existingIngredientIds = productEntity.ProductIngredients!.Select(pi => pi.IngredientId).ToList();
        List<int>? ingredientIdsToUpdate = productUpdateDto.IngredientIds.ToSafeList();

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
        AddIngredients(productEntity, ingredientIdsToUpdate);
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

    protected async Task<ProductEntity> GetProductById(int productId)
    {
        ProductEntity? productEntity = await _unitOfWork.Products.GetByIdAsync(productId);

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

}
