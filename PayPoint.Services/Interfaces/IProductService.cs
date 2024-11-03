using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int productId, ProductDto productDto);
    Task<IEnumerable<Product>> GetProductsAsync(int? categoryId, ProductDto productDto);
    Task<Product?> AddProductAsync(ProductCreateDto productCreateDto);
    Task<bool> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
}
