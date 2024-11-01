using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Entities;
using PayPoint.Core.Enums;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class SubCategoryService : BaseService, ISubCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SubCategory?> GetSubCategoryByIdAsync(int SubCategoryId)
    {
        IQueryable<SubCategoryEntity> query = _unitOfWork.SubCategories.AsQueryable().Include(x => x.Category);

        SubCategoryEntity? subCategoryEntity = await query.FirstOrDefaultAsync(c => c.SubCategoryId == SubCategoryId);

        if (subCategoryEntity.IsNullOrEmpty())
        {
            return null;
        }

        return _mapper.Map<SubCategory>(subCategoryEntity);
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync()
    {
        IQueryable<SubCategoryEntity> query = _unitOfWork.SubCategories.AsQueryable().Include(x => x.Category);
        IEnumerable<SubCategoryEntity?> subCategories = await query.ToListAsync();

        return _mapper.Map<IEnumerable<SubCategory>>(subCategories);
    }

    public async Task<SubCategory?> AddSubCategoryAsync(SubCategoryCreateDto categoryCreateDto)
    {
        // validate if category exists
        _ = await GetCategoryById(categoryCreateDto.CategoryId!.Value);

        SubCategoryEntity subCategoryEntity = _mapper.Map<SubCategoryEntity>(categoryCreateDto);
        subCategoryEntity.Status = CategoryStatus.Active;

        await _unitOfWork.SubCategories.AddAsync(subCategoryEntity);
        int? rowInserted = await _unitOfWork.SaveChangesAsync();

        if (rowInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<SubCategory>(subCategoryEntity);
    }

    public async Task<bool> UpdateSubCategoryAsync(int id, SubCategoryUpdateDto SubCategoryUpdateDto)
    {
        SubCategoryEntity subCategoryEntity = await GetSubCategoryById(id);
        subCategoryEntity = _mapper.Map(SubCategoryUpdateDto, subCategoryEntity);

        _unitOfWork.SubCategories.Update(subCategoryEntity);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }

    public async Task<bool> DeleteSubCategoryAsync(int id)
    {
        _ = await GetSubCategoryById(id);

        await _unitOfWork.SubCategories.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }
}
