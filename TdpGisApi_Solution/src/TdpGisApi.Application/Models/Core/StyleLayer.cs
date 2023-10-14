using Newtonsoft.Json.Linq;

namespace TdpGisApi.Application.Models.Core;

public class StyleLayer
{
    public StyleLayer(StyleType type)
    {
        Type = type;
        Paint = new JObject();
    }

    public StyleType Type { get; set; }
    public JObject Paint { get; set; }

    public int? MinZoom { get; set; }

    public int? MaxZoom { get; set; }

    public void AddPaintProperty(string propertyName, object value)
    {
        Paint.Add(propertyName, new JValue(value));
    }
}