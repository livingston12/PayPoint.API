using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Tables;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class TableController : BaseController
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTables()
    {
        IEnumerable<Table>? tables = await _tableService.GetTablesAsync();

        return Ok(tables);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTableById(int id)
    {
        Table? table = await _tableService.GetTableByIdAsync(id);

        if (table.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(table);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        bool isDeleted = await _tableService.DeleteTableAsync(id);
        
        if (!isDeleted)
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddTable([FromBody] TableCreateDto tableCreateDto)
    {
        Table? table = await _tableService.AddTableAsync(tableCreateDto);

        if (table.IsNullOrEmpty())
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }

        return CreatedAtAction(nameof(GetTableById), new { id = table!.TableId }, table);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTable(int id, [FromBody] TableUpdateDto tableUpdateDto)
    {
        bool isUpdated = await _tableService.UpdateTableAsync(id, tableUpdateDto);
        if (!isUpdated)
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }
        return Ok();
    }
}
