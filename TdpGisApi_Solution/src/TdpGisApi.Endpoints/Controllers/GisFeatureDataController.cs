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
    [Route("searchbyphrase/{feature:Guid}/{text}")]
    public async Task<IActionResult> FeaturesByText(Guid feature, string text)
    {
        var result = await _gisFeatureDataHandler.GetFeatureDataByText(feature, text);
        return Ok(result);
    }

    [HttpGet]
    [Route("pagingsearchbyphrase/{feature:Guid}/{text}/{pageSize:int:min(10)}/{pageNumber:int:min(1)}/{token?}")]
    public async Task<IActionResult> PagingFeaturesByText(Guid feature, string text, int pageSize, int pageNumber, string? token = null)
    {
        var result = await _gisFeatureDataHandler.GetPagingFeatureDataByText(feature, text, pageSize, pageNumber, token);
        return Ok(result);
    }
}