﻿using AutoMapper;
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

    public async Task<List<QueryConfigLite>> GetFeatureLite()
    {
        var features = await _gisAppFactory.CreateAppFeatureData();
        var featureLite = _mapper.Map<List<QueryConfigLite>>(features.Features);
        return featureLite;
    }

    public async Task<List<FeatureLayerDto>> GetLayerDtos()
    {
        var features = await _gisAppFactory.CreateAppFeatureData();
        var featureDto = _mapper.Map<List<FeatureLayerDto>>(features.Layers);
        return featureDto;
    }

    public async Task<List<FeatureLayerLite>> GetLayerLite()
    {
        var features = await _gisAppFactory.CreateAppFeatureData();
        var featureLite = _mapper.Map<List<FeatureLayerLite>>(features.Layers);
        return featureLite;
    }
}