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
        CreateMap<ProductEntity, Product>()
        .ForMember(dest => dest.Ingredients, opt =>
        {
            opt.PreCondition(src => src.ProductIngredients.IsNotNullOrEmpty() && src.ProductIngredients!.Any());
            opt.MapFrom(src => src.ProductIngredients!.Select(x => x.Ingredient).ToList());
        })
            // If SubCategoryId is 0, it means that the subcategory was not created yet.
        .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src =>
                    src.SubCategory.SubCategoryId == 0 && src.SubCategory.Name.IsNullOrEmpty()
                    ? null
                    : src.SubCategory));

        CreateMap<CategoryCreateDto, CategoryEntity>();
        CreateMap<CategoryUpdateDto, CategoryEntity>();
        CreateMap<CategoryEntity, Category>();
        CreateMap<CategoryEntity, CategoryDto>();

        CreateMap<SubCategoryCreateDto, SubCategoryEntity>();
        CreateMap<SubCategoryUpdateDto, SubCategoryEntity>();
        CreateMap<SubCategoryEntity, SubCategory>();
        CreateMap<CategoryEntity, SubCategoryCategory>();
        CreateMap<SubCategoryEntity, SubCategoryDto>();


        CreateMap<IngredientEntity, IngredientDto>();
        CreateMap<IngredientCreateDto, IngredientEntity>();
        CreateMap<IngredientUpdateDto, IngredientEntity>();
        CreateMap<IngredientEntity, Ingredient>();
    }
}
