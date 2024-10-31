
using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        IEnumerable<Product> products = await _productService.GetProductsAsync();
        
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id, [FromQuery] ProductDto productDto)
    {
        Product? product = await _productService.GetProductByIdAsync(id, productDto);
        
        return Ok(product);
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetProductsByCategoryId(int id)
    {
        IEnumerable<Product> products = await _productService.GetProductsByCategoryIdAsync(id);
        
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        await _productService.AddProductAsync(productCreateDto);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateProductAsync(id, productUpdateDto);
        
        return Ok();
    }
}
