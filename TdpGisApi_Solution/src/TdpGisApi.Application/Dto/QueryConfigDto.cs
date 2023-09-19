using TdpGisApi.Domain.Models;
using GeometryType = Microsoft.Azure.Cosmos.Spatial.GeometryType;
using QueryType = DnsClient.QueryType;

namespace TdpGisApi.Application.Dto;

public class QueryConfigDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public QueryType QueryType { get; set; }

    public required string QueryField { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }

    public GeometryType GeometryType { get; set; }
    public Guid ConnectionId { get; set; }
}