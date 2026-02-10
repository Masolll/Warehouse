using Microsoft.AspNetCore.Mvc;
using Warehouse.Models.Domain;
using Warehouse.Models.Dtos;
using Warehouse.Services;

namespace Warehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoilController : ControllerBase
{
    private readonly CoilService _service;
    
    public CoilController(CoilService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Coil>> Create(CreateCoilDto coilDto)
    {
        var coil = await _service.CreateAsync(coilDto);
        return Ok(coil);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Coil>> Delete(Guid id)
    {
        var coil = await _service.MarkAsDeletedAsync(id);
        return Ok(coil);
    }

    [HttpGet]
    public ActionResult<List<Coil>> Get([FromQuery] CoilFilterDto filterDto)
    {
        var coils = _service.GetFiltered(filterDto);
        return Ok(coils);
    }
}