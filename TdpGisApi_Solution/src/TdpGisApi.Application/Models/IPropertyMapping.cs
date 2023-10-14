using TdpGisApi.Application.Models.Core;

namespace TdpGisApi.Application.Models;

public interface IPropertyMapping
{
    Guid Id { get; set; }
    List<PropertyOutput> Mappings { get; set; }
}