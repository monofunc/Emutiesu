using Emutiesu.Models;
using Emutiesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emutiesu.Controllers;

[Produces("application/json")]
[Route("api/reports")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IProductReportService _productReportService;

    public ReportsController(IProductReportService productReportService)
    {
        _productReportService = productReportService;
    }

    /// <summary>
    ///     Get Report of Product quantity and amount grouped by types and categories
    /// </summary>
    /// <response code="200">Report table</response>
    [ProducesResponseType(typeof(List<ProductReportItem>), StatusCodes.Status200OK)]
    [HttpGet("orders")]
    public async Task<ActionResult<List<ProductReportItem>>> GetProductReportAsync()
    {
        return await _productReportService.GenerateReportAsync();
    }
}
