using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TdpGisApi.Application.Dto;
using TdpGisApi.Application.Factory;
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
}