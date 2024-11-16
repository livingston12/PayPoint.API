using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class CategoryService : BaseService, ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Category?> GetCategoryByIdAsync(int CategoryId, CategoryInputDto CategoryDto)
    {
        IQueryable<CategoryEntity> query = _unitOfWork.Categories.AsQueryable();

        if (CategoryDto.IncludeSubCategories == true)
        {
            query = query.Include(c => c.SubCategories);
        }

        CategoryEntity? categoryEntity = await query.FirstOrDefaultAsync(c => c.CategoryId == CategoryId);

        if (categoryEntity is null)
        {
            return null;
        }

        return _mapper.Map<Category>(categoryEntity);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CategoryInputDto CategoryDto)
    {
        IQueryable<CategoryEntity> query = _unitOfWork.Categories.AsQueryable();

        if (CategoryDto.IncludeSubCategories == true)
        {
            query = query.Include(c => c.SubCategories);
        }

        IEnumerable<CategoryEntity?> categories = await query.ToListAsync();

        return _mapper.Map<IEnumerable<Category>>(categories);
    }

    public async Task<Category?> AddCategoryAsync(CategoryCreateDto CategoryCreateDto)
    {
        CategoryEntity categoryEntity = _mapper.Map<CategoryEntity>(CategoryCreateDto);
        categoryEntity.Status = Core.Enums.CategoryStatus.Active;
        
        await _unitOfWork.Categories.AddAsync(categoryEntity);
        int? rowInserted = await _unitOfWork.SaveChangesAsync();

        if (rowInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Category>(categoryEntity);
    }

    public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto CategoryUpdateDto)
    {
        CategoryEntity categoryEntity = await GetCategoryById(id);

        _mapper.Map(CategoryUpdateDto, categoryEntity);

        _unitOfWork.Categories.Update(categoryEntity);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        _ = await GetCategoryById(id);

        await _unitOfWork.Categories.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }
}
