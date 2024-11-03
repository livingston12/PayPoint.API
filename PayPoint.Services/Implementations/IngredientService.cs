using AutoMapper;
using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class IngredientService : BaseService, IIngredientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
    {
        IEnumerable<IngredientEntity>? ingredients = await _unitOfWork.Ingredients.GetAllAsync();

        return _mapper.Map<IEnumerable<Ingredient>>(ingredients);
    }

    public async Task<Ingredient?> GetIngredientByIdAsync(int IngredientId)
    {
        IngredientEntity? ingredient = await _unitOfWork.Ingredients.GetByIdAsync(IngredientId);

        return _mapper.Map<Ingredient>(ingredient);
    }

    public async Task<Ingredient?> AddIngredientAsync(IngredientCreateDto IngredientCreateDto)
    {
        IngredientEntity? ingredientEntity = _mapper.Map<IngredientEntity>(IngredientCreateDto);

        if (ingredientEntity.IsNullOrEmpty())
        {
            return null;
        }

        await _unitOfWork.Ingredients.AddAsync(ingredientEntity);
        int? rowInserted = await _unitOfWork.SaveChangesAsync();

        if (rowInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Ingredient>(ingredientEntity);
    }

    public async Task<bool> UpdateIngredientAsync(int id, IngredientUpdateDto IngredientUpdateDto)
    {
        IngredientEntity? ingredientEntity = await GetIngredientById(id);

        if (ingredientEntity.IsNullOrEmpty())
        {
            return false;
        }

        _mapper.Map(IngredientUpdateDto, ingredientEntity);
        _unitOfWork.Ingredients.Update(ingredientEntity);

        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        if (rowsUpdated.IsLessThanOrEqualTo(0))
        {
            return false;
        }

        return rowsUpdated.IsGreaterThan(0);
    }

    public async Task<bool> DeleteIngredientAsync(int id)
    {
        _ = await GetIngredientById(id);

        await _unitOfWork.Ingredients.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }
}
