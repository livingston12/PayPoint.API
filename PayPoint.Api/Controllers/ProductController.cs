
using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.Extensions;
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
    public async Task<IActionResult> GetProducts([FromHeader(Name = "CategoryId")] int? CategoryId, [FromHeader(Name = "IncludeIngredients")][FromQuery] ProductDto productDto)
    {
        IEnumerable<Product> products = await _productService.GetProductsAsync(CategoryId, productDto);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id,
        [FromQuery(Name = "IncludeIngredients")] bool? includeIngredients,
        [FromHeader(Name = "ExcludeSubCategory")] bool? ExcludeSubCategory)
    {
        ProductDto productDto = new()
        {
            IncludeIngredients = includeIngredients == true,
            IncludeSubCategory = ExcludeSubCategory == false
        };

        Product? product = await _productService.GetProductByIdAsync(id, productDto);

        if (product.IsNullOrEmpty())
        {
            return NotFound("Producto no encontrado.");
        }

        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
    {
        IEnumerable<Product> products = await _productService.GetProductsByCategoryIdAsync(categoryId);

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        Product? product = await _productService.AddProductAsync(productCreateDto);

        if (product.IsNullOrEmpty())
        {
            return BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        bool isDeleted = await _productService.DeleteProductAsync(id);

        if (!isDeleted)
        {
            return BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        bool isUpdated = await _productService.UpdateProductAsync(id, productUpdateDto);

        if (!isUpdated)
        {
            return BadRequest(ErrorMessageBadRequest);
        }

        return Ok();
    }
}
