using AutoMapper;
using TdpGisApi.Application.Dto;
using TdpGisApi.Application.Factory;

namespace TdpGisApi.Application.Handlers.Core;

public class GisFeatureInfoHandler : IGisFeatureInfoHandler
{
    private readonly IGisAppFactory _gisAppFactory;
    private readonly IMapper _mapper;

    public GisFeatureInfoHandler(IGisAppFactory gisAppFactory, IMapper mapper)
    {
        _gisAppFactory = gisAppFactory;
        _mapper = mapper;
    }

    public async Task<List<QueryConfigDto>> GetFeatureDtos()
    {
        var features = await _gisAppFactory.CreateAppFeatureData();
        var featureDto = _mapper.Map<List<QueryConfigDto>>(features.Features);
        return featureDto;
    }
}