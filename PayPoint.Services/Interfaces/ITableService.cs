using PayPoint.Core.DTOs.Tables;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface ITableService
{
    Task<Table?> GetTableByIdAsync(int TableId);
    Task<IEnumerable<Table>?> GetTablesAsync();
    Task<Table?> AddTableAsync(TableCreateDto TableCreateDto);
    Task<bool> UpdateTableAsync(int id, TableUpdateDto TableUpdateDto);
    Task<bool> DeleteTableAsync(int id);
}
