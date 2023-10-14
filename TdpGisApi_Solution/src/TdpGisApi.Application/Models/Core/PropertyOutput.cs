namespace TdpGisApi.Application.Models.Core;

public class PropertyOutput
{
    public Guid Id { get; set; }
    public required PropertyType PropertyType { get; set; } = PropertyType.Normal;
    public required string PropertyName { get; set; }
    public required string OutputName { get; set; }
    public required ShowLevel ShowLevel { get; set; } = ShowLevel.Private;
}