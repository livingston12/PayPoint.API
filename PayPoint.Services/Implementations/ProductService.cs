using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Entities;
using PayPoint.Core.Enums;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class ProductService : BaseService, IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Product?> GetProductByIdAsync(int productId, ProductDto productDto)
    {
        if (productDto is null || productId <= 0) return null;

        IQueryable<ProductEntity> query = _unitOfWork.Products.AsQueryable();

        query = ApplyIncludes(query, productDto, null);

        ProductEntity? productEntity = await query.FirstOrDefaultAsync(x => x.ProductId == productId);

        if (productEntity.IsNullOrEmpty())
        {
            return null;
        }

        Product product = _mapper.Map<Product>(productEntity);

        if (productDto.IncludeSubCategory == false)
        {
            product!.SubCategory = null;
        }

        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int? categoryId, ProductDto productDto)
    {
        IQueryable<ProductEntity> query = _unitOfWork.Products.AsQueryable();

        query = ApplyIncludes(query, productDto, categoryId);

        IEnumerable<ProductEntity?> productsEntity = await query.ToListAsync();

        if (productsEntity.IsNullOrEmpty())
        {
            return new List<Product>();
        }

        return _mapper.Map<IEnumerable<Product>>(productsEntity);
    }

    private IQueryable<ProductEntity> ApplyIncludes(IQueryable<ProductEntity> query, ProductDto productDto, int? categoryId)
    {
        bool isCategoryIdFiltered = categoryId.HasValue && categoryId > 0;
        productDto.IncludeCategories = isCategoryIdFiltered || productDto.IncludeCategories == true;

        // Include SubCategory and Category.
        if (productDto.IncludeAll == true || productDto.IncludeCategories == true)
        {
            query = query.Include(x => x.SubCategory.Category);

            if (isCategoryIdFiltered)
            {
                query = query.Where(x => x.SubCategory.CategoryId == categoryId!.Value);
            }
        }
        else if (productDto.IncludeSubCategory == true)
        {
            query = query.Include(x => x.SubCategory);
        }

        // Include Ingredients.
        if (productDto.IncludeAll == true || productDto.IncludeIngredients == true)
        {
            query = query
                        .Where(x => x.ProductIngredients != null)
                        .Include(x => x.ProductIngredients!)
                        .ThenInclude(x => x.Ingredient);
        }

        return query;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        IEnumerable<ProductEntity?> productsEntity = await _unitOfWork.Products.GetProductsByCategoryIdAsync(categoryId);

        if (productsEntity.IsNullOrEmpty())
        {
            return new List<Product>();
        }

        return _mapper.Map<IEnumerable<Product>>(productsEntity);
    }

    public async Task<Product?> AddProductAsync(ProductCreateDto productCreateDto)
    {
        SubCategoryEntity subCategory = await GetSubCategoryById(productCreateDto.SubCategoryId!.Value);

        ProductEntity productEntity = _mapper.Map<ProductEntity>(productCreateDto);
        productEntity.Status = ProductStatus.Available;
        productEntity.SubCategory = subCategory;

        if (productCreateDto.IngredientIds.IsNotNullOrEmpty())
        {
            productEntity.HasIngredients = true;

            if (productEntity.ProductIngredients.IsNullOrEmpty())
            {
                productEntity.ProductIngredients = new List<ProductIngredientEntity>();
            }

            await AddIngredientsToProduct(productEntity, productCreateDto.IngredientIds.ToSafeList());
        }

        await _unitOfWork.Products.AddAsync(productEntity);
        int? rowInserted = await _unitOfWork.SaveChangesAsync();

        if (rowInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Product>(productEntity);
    }

    public async Task<Product?> AddUpdateProductIngredientAsync(int id, IEnumerable<ProductIngredientCreateDto> productIngredientCreateDto)
    {
        ProductEntity productEntity = await GetProductById(id, true);

        if (productEntity.ProductIngredients.IsNullOrEmpty())
        {
            productEntity.ProductIngredients = new List<ProductIngredientEntity>();
        }

        Dictionary<int, int?> ingredientQuantityIds = productIngredientCreateDto.ToDictionary(x => x.IngredientId!.Value, x => x.Quantity);

        // Add new ingredients or update existing.
        await UpdateIngredientsToProduct(productEntity, ingredientQuantityIds);

        int? rowsAffected = await _unitOfWork.SaveChangesAsync();

        if (rowsAffected.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Product>(productEntity);
    }

    public async Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
    {
        ProductEntity productEntity = await GetProductById(id, true);
        IEnumerable<int>? ingredientIds = productUpdateDto.IngredientIds;

        if (ingredientIds.IsNotNullOrEmpty() && productEntity.ProductIngredients.IsNotNullOrEmpty())
        {
            productEntity.HasIngredients = true;

            if (productEntity.ProductIngredients.IsNullOrEmpty())
            {
                productEntity.ProductIngredients = new List<ProductIngredientEntity>();
            }

            UpdateRemoveIngredientsToProduct(productEntity, ingredientIds!);
        }

        _mapper.Map(productUpdateDto, productEntity);
        _unitOfWork.Products.Update(productEntity!);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        _ = await GetProductById(id);

        await _unitOfWork.Products.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }
}
