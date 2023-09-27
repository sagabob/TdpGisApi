using Microsoft.AspNetCore.Mvc;
using TdpGisApi.Application.Handlers;

namespace TdpGisApi.Endpoints.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GisFeatureDataController : ControllerBase
{
    private readonly IGisFeatureDataHandler _gisFeatureDataHandler;

    public GisFeatureDataController(IGisFeatureDataHandler gisFeatureDataHandler)
    {
        _gisFeatureDataHandler = gisFeatureDataHandler;
    }

    [HttpGet]
    [Route("bytext/{feature:Guid}/{text}")]
    public async Task<IActionResult> FeaturesByText(Guid feature, string text)
    {
        var result = await _gisFeatureDataHandler.GetFeatureDataByText(feature, text);
        return Ok(result);
    }
}