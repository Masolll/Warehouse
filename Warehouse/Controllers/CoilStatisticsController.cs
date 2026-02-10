using Microsoft.AspNetCore.Mvc;
using Warehouse.Models.Dtos;
using Warehouse.Services;

namespace Warehouse.Controllers;

[ApiController]
[Route("api/Coil/Statistics")]
public class CoilStatisticsController : ControllerBase
{
    private readonly CoilStatisticsService _service;
    
    public CoilStatisticsController(CoilStatisticsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<CoilStatisticsDto>> Get([FromQuery]PeriodDto period)
    {
        if (period.PeriodStart > period.PeriodEnd)
            throw new ArgumentException("Period start must be earlier than period end");

        if (period.PeriodEnd > DateTime.UtcNow)
            throw new ArgumentException("Period end must be earlier or equal than current date");
        
        var statistics = _service.GetStatistics(period);
        return Ok(statistics);
    }
}