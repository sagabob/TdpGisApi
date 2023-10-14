using Microsoft.AspNetCore.Mvc;
using TdpGisApi.Application.Handlers;

namespace TdpGisApi.Endpoints.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GisFeatureInfoController : ControllerBase
{
    private readonly IGisFeatureInfoHandler _gisFeatureInfoHandler;


    public GisFeatureInfoController(IGisFeatureInfoHandler gisFeatureInfoHandler)
    {
        _gisFeatureInfoHandler = gisFeatureInfoHandler;
    }

    [HttpGet]
    [Route("instances")]
    public async Task<IActionResult> Instances()
    {
        var featureDto = await _gisFeatureInfoHandler.GetFeatureDtos();

        return Ok(featureDto);
    }


    [HttpGet]
    [Route("features")]
    public async Task<IActionResult> Features()
    {
        var featureLite = await _gisFeatureInfoHandler.GetFeatureLite();

        return Ok(featureLite);
    }

    [HttpGet]
    [Route("layerinstances")]
    public async Task<IActionResult> LayerInstances()
    {
        var featureDto = await _gisFeatureInfoHandler.GetLayerDtos();

        return Ok(featureDto);
    }


    [HttpGet]
    [Route("layers")]
    public async Task<IActionResult> Layers()
    {
        var featureLite = await _gisFeatureInfoHandler.GetLayerLite();

        return Ok(featureLite);
    }
}