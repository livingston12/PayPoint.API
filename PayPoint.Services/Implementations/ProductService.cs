using System.Linq.Expressions;
using AutoMapper;
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

        ProductEntity? productEntity = null;

        List<Expression<Func<ProductEntity, object>>> includes = new List<Expression<Func<ProductEntity, object>>>();

        if (productDto.IncludeSubCategory == true)
        {
            includes.Add(x => x.SubCategory);
        }

        if (productDto.IncludeIngredients == true)
        {
            includes.Add(x => x.ProductIngredients ?? new List<ProductIngredientEntity>());
        }

        productEntity = await _unitOfWork.Products.GetByIdWithIncludeAsync(productId, includes.ToArray());

        if (productEntity is null)
        {
            return null;
        }

        return _mapper.Map<Product>(productEntity);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        IEnumerable<ProductEntity?> productsEntity = await _unitOfWork.Products.GetAllAsync();

        if (productsEntity.IsNullOrEmpty())
        {
            return new List<Product>();
        }

        return _mapper.Map<IEnumerable<Product>>(productsEntity);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        IEnumerable<ProductEntity?> productsEntity = await _unitOfWork.Products.GetProductsBySubCategoryIdAsync(categoryId);

        if (productsEntity.IsNullOrEmpty())
        {
            return new List<Product>();
        }

        return _mapper.Map<IEnumerable<Product>>(productsEntity);
    }

    public async Task AddProductAsync(ProductCreateDto productCreateDto)
    {
        _ = await GetSubCategoryById(productCreateDto.SubCategoryId!.Value);

        ProductEntity productEntity = _mapper.Map<ProductEntity>(productCreateDto);
        productEntity.Status = ProductStatus.Available;

        if (productCreateDto.IngredientIds.IsNotNullOrEmpty() && !productEntity.HasIngredients)
        {
            productEntity.HasIngredients = true;

            if (productEntity.ProductIngredients.IsNullOrEmpty())
            {
                productEntity.ProductIngredients = new List<ProductIngredientEntity>();
            }

            AddIngredients(productEntity, productCreateDto.IngredientIds.ToSafeList());
        }

        await _unitOfWork.Products.AddAsync(productEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        _ = await GetProductById(id);

        await _unitOfWork.Products.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
    {
        ProductEntity productEntity = await GetProductById(id);

        if (productUpdateDto.IngredientIds.IsNotNullOrEmpty() && productEntity.HasIngredients)
        {
            productEntity.HasIngredients = true;

            if (productEntity.ProductIngredients.IsNullOrEmpty())
            {
                productEntity.ProductIngredients = new List<ProductIngredientEntity>();
            }

            UpdateIngredients(productEntity, productUpdateDto);
        }

        _mapper.Map(productUpdateDto, productEntity);

        if (productEntity.IsNullOrEmpty())
        {
            throw new Exception("Cannot update product");
        }

        _unitOfWork.Products.Update(productEntity!);
        await _unitOfWork.SaveChangesAsync();
    }
}
