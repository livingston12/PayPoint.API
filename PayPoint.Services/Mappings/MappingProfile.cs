using AutoMapper;
using PayPoint.Core.DTOs;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.DTOs.Orders;
using PayPoint.Core.DTOs.Products;
using PayPoint.Core.DTOs.Rooms;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.DTOs.Tables;
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
            opt.MapFrom(src => GetIngredientsFromProductEntity(src));
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

        CreateMap<RoomCreateDto, RoomEntity>();
        CreateMap<RoomUpdateDto, RoomEntity>();
        CreateMap<RoomEntity, Room>();

        CreateMap<TableCreateDto, TableEntity>();
        CreateMap<TableUpdateDto, TableEntity>();
        CreateMap<TableEntity, Table>();
        CreateMap<TableEntity, TableDto>();

        CreateMap<OrderCreateDto, OrderEntity>()
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
        CreateMap<OrderUpdateDto, OrderEntity>();
        CreateMap<OrderEntity, Order>();
    }

    private object GetIngredientsFromProductEntity(ProductEntity src)
    {
        return src.ProductIngredients!
                .Select(x => new IngredientDto
                {
                    IngredientId = x.Ingredient.IngredientId,
                    Name = x.Ingredient.Name,
                    Quantity = x.Quantity
                })
                .ToList();
    }
}
