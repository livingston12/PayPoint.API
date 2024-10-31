using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int productId, ProductDto productDto);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task AddProductAsync(ProductCreateDto productCreateDto);
    Task UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
}
