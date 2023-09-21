namespace TdpGisApi.Application.Models;

public class PropertyOutput
{
    public Guid Id { get; set; }
    public required PropertyType PropertyType { get; set; }
    public required string PropertyName { get; set; }
    public required string OutputName { get; set; }
}