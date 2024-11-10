using AutoMapper;
using PayPoint.Core.DTOs.Tables;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class TableService : BaseService , ITableService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TableService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Table>?> GetTablesAsync()
    {
        IEnumerable<TableEntity>? tablesEntity = await _unitOfWork.Tables.GetAllAsync();

        return _mapper.Map<IEnumerable<Table>>(tablesEntity);
    }

    public async Task<Table?> GetTableByIdAsync(int TableId)
    {
        TableEntity? table = await _unitOfWork.Tables.GetByIdAsync(TableId);

        return _mapper.Map<Table>(table);
    }

    public async Task<Table?> AddTableAsync(TableCreateDto TableCreateDto)
    {
        TableEntity tableEntity = _mapper.Map<TableEntity>(TableCreateDto);
        
        await _unitOfWork.Tables.AddAsync(tableEntity);
        int? rowsInserted = await _unitOfWork.SaveChangesAsync();

        if (rowsInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Table>(tableEntity);
    }

    public async Task<bool> UpdateTableAsync(int id, TableUpdateDto TableUpdateDto)
    {
        TableEntity tableEntity = await GetTableById(id);
        tableEntity = _mapper.Map(TableUpdateDto, tableEntity);

        _unitOfWork.Tables.Update(tableEntity);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        if (rowsUpdated.IsLessThanOrEqualTo(0))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteTableAsync(int id)
    {
        _ = await GetTableById(id);

        await _unitOfWork.Tables.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        if (rowsDeleted.IsLessThanOrEqualTo(0))
        {
            return false;
        }

        return true;
    }
}
