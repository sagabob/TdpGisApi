using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TdpGisApi.Application.Dto;
using TdpGisApi.Application.Factory;

namespace TdpGisApi.Endpoints.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GisFeatureInfoController : ControllerBase
{
    private readonly IGisAppFactory _gisAppFactory;
    private readonly IMapper _mapper;

    public GisFeatureInfoController(IGisAppFactory gisAppFactory, IMapper mapper)
    {
        _gisAppFactory = gisAppFactory;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("instances")]
    public async Task<IActionResult> Instances()
    {
        var features = await _gisAppFactory.CreateAppFeatureData();
        var featureDto = _mapper.Map<List<QueryConfigDto>>(features.Features);
        return Ok(featureDto);
    }
}