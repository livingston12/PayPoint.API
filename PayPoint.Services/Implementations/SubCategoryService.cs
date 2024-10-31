using AutoMapper;
using PayPoint.Core.DTOs.SubCategories;
using PayPoint.Core.Entities;
using PayPoint.Core.Enums;
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
        SubCategoryEntity subCategoryEntity = await GetSubCategoryById(SubCategoryId);

        return _mapper.Map<SubCategory>(subCategoryEntity);
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync()
    {
        IEnumerable<SubCategoryEntity?> subCategories = await _unitOfWork.SubCategories.GetAllAsync();

        return _mapper.Map<IEnumerable<SubCategory>>(subCategories);
    }

    public async Task AddSubCategoryAsync(SubCategoryCreateDto categoryCreateDto)
    {
        // validate if category exists
        _ = await GetCategoryById(categoryCreateDto.CategoryId!.Value);
    
        SubCategoryEntity subCategoryEntity = _mapper.Map<SubCategoryEntity>(categoryCreateDto);
        subCategoryEntity.Status = CategoryStatus.Active;

        await _unitOfWork.SubCategories.AddAsync(subCategoryEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateSubCategoryAsync(int id, SubCategoryUpdateDto SubCategoryUpdateDto)
    {
        SubCategoryEntity subCategoryEntity = await GetSubCategoryById(id);
        subCategoryEntity = _mapper.Map(SubCategoryUpdateDto, subCategoryEntity);

        _unitOfWork.SubCategories.Update(subCategoryEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSubCategoryAsync(int id)
    {
        _ = await GetSubCategoryById(id);

        await _unitOfWork.SubCategories.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
