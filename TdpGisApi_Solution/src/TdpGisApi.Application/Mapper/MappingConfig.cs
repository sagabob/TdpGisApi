﻿using AutoMapper;
using TdpGisApi.Application.Dto;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Mapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<QueryConfig, QueryConfigDto>().ForMember(x => x.ConnectionId, s => s.MapFrom(x => x.Connection.Id));
    }
}