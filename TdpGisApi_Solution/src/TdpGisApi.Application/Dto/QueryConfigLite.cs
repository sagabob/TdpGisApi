using TdpGisApi.Application.Models.Core;

namespace TdpGisApi.Application.Dto;

public class QueryConfigLite
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public QueryType QueryType { get; set; }

    public required string QueryField { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }
    public GeometryType GeometryType { get; set; }
}