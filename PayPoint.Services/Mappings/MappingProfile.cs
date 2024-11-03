using AutoMapper;
using PayPoint.Core.DTOs;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;

namespace PayPoint.Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductCreateDto, ProductEntity>();
        CreateMap<ProductUpdateDto, ProductEntity>();
        CreateMap<ProductEntity, Product>();

        CreateMap<CategoryCreateDto, CategoryEntity>();
        CreateMap<CategoryUpdateDto, CategoryEntity>();
        CreateMap<CategoryEntity, Category>();

        CreateMap<SubCategoryCreateDto, SubCategoryEntity>();
        CreateMap<SubCategoryUpdateDto, SubCategoryEntity>();
        CreateMap<SubCategoryEntity, SubCategory>();
        CreateMap<CategoryEntity, SubCategoryCategory>();
        CreateMap<SubCategoryEntity, SubCategoryDto>();


        CreateMap<IngredientEntity, IngredientDto>();
    }
}
