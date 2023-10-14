using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
    public async Task<IActionResult> PagingFeaturesByText(Guid feature, string text, int pageSize, int pageNumber,
        string? token = null)
    {
        var result =
            await _gisFeatureDataHandler.GetPagingFeatureDataByText(feature, text, pageSize, pageNumber, token);
        return Ok(result);
    }


    [HttpGet]
    [Route("getallfeaturedata/{feature:Guid}")]
    public async Task<IActionResult> GetAllFeatureData(Guid feature)
    {
        var result = await _gisFeatureDataHandler.GetAllFeatureData(feature);
        return Ok(result);
    }


    [HttpGet]
    [Route("getalllayerdata/{layer:Guid}")]
    public async Task<IActionResult> GetAllLayerData(Guid layer)
    {
        var result = await _gisFeatureDataHandler.GetAllLayerData(layer);
        return Ok(result);
    }

    [HttpPost]
    [Route("allspatialdata/{feature:Guid}")]
    public async Task<IActionResult> GetAllSpatialData(Guid feature, [FromBody] string boundaries)
    {
        var joBoundaries = JObject.Parse(boundaries);
        var results = await _gisFeatureDataHandler.GetSpatialData(feature, joBoundaries);
        return Ok(results);
    }


    [HttpPost]
    [Route("allspatialdatasinglebounary/{feature:Guid}")]
    public async Task<IActionResult> GetAllSpatialDataSingleBoundary(Guid feature, [FromBody] string boundaries)
    {
        var joBoundaries = JObject.Parse(boundaries);
        var results = await _gisFeatureDataHandler.GetSpatialDataSingleBoundary(feature, joBoundaries);
        return Ok(results);
    }
}