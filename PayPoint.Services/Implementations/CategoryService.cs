using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.Categories;
using PayPoint.Core.Entities;
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

    public async Task<Category?> GetCategoryByIdAsync(int CategoryId, CategoryDto CategoryDto)
    {
        if (CategoryDto is null || CategoryId <= 0) return null;

        IQueryable<CategoryEntity> query = _unitOfWork.Categories.AsQueryable();

        if (CategoryDto.IncludeSubCategories == true)
        {
            query = query.Include(c => c.SubCategories);
        }

        CategoryEntity? categoryEntity = await query.FirstOrDefaultAsync(c => c.Id == CategoryId);

        if (categoryEntity is null)
        {
            return null;
        }

        return _mapper.Map<Category>(categoryEntity);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CategoryDto CategoryDto)
    {
        IQueryable<CategoryEntity> query = _unitOfWork.Categories.AsQueryable();

        if (CategoryDto.IncludeSubCategories == true)
        {
            query = query.Include(c => c.SubCategories);
        }

        IEnumerable<CategoryEntity?> categories = await query.ToListAsync();

        return _mapper.Map<IEnumerable<Category>>(categories);
    }

    public async Task AddCategoryAsync(CategoryCreateDto CategoryCreateDto)
    {
        CategoryEntity categoryEntity = _mapper.Map<CategoryEntity>(CategoryCreateDto);
        categoryEntity.Status = Core.Enums.CategoryStatus.Active;
        
        await _unitOfWork.Categories.AddAsync(categoryEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(int id, CategoryUpdateDto CategoryUpdateDto)
    {
        CategoryEntity categoryEntity = await GetCategoryById(id);

        _mapper.Map(CategoryUpdateDto, categoryEntity);

        _unitOfWork.Categories.Update(categoryEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        _ = await GetCategoryById(id);

        await _unitOfWork.Categories.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
