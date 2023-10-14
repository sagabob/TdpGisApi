using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TdpGisApi.Application.Context;
using TdpGisApi.Application.Models.Core;

namespace SampleApp;

public class DbService
{
    private readonly IConfiguration _configuration;
    private readonly IDbContextFactory<CosmosGisAppContext> _contextFactory;
    private readonly CosmosClient _cosmosClient;

    public DbService(IDbContextFactory<CosmosGisAppContext> contextFactory, CosmosClient cosmosClient,
        IConfiguration configuration)
    {
        _contextFactory = contextFactory;
        _cosmosClient = cosmosClient;
        _configuration = configuration;
    }

    private async Task RecreateDatabase()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        SetIndex();
    }

    private void SetIndex()
    {
        _cosmosClient.GetDatabase("GisApp")
            .DefineContainer("AppConnections", "/DbType")
            .WithUniqueKey()
            .Path("/Name")
            .Attach();
    }

    public async Task ImportCleanSamples()
    {
        await RecreateDatabase();

        await using var defaultContext = await _contextFactory.CreateDbContextAsync();

        await AddItemsFromDefaultContext(defaultContext);
    }


    private async Task AddItemsFromDefaultContext(CosmosGisAppContext defaultContext)
    {
        IDictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "URL", _configuration["URL"]! },
            { "KEY", _configuration["KEY"]! }
        };
        var conn = new ConnectionObject
        {
            Id = new Guid(),
            DatabaseId = "GeoDatabases",
            ConnectionParameters = parameters,
            ConnectionType = ConnectionType.ConnectionString,
            DbType = DbType.Cosmosdb,
            Name = "Main Connection",
            IsDisabled = false
        };

        var idProperty = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "id",
            OutputName = "id",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var park1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkName",
            OutputName = "title",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var park2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkTypeDescription",
            OutputName = "type",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var park3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Area",
            OutputName = "area",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };
        var park4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "geometry",
            PropertyType = PropertyType.Spatial,
            ShowLevel = ShowLevel.Public
        };

        var parkFeature = new QueryConfig
        {
            Id = new Guid(),
            Name = "Parks",
            DisplayName = "Parks",
            CollectionName = "Parks",
            PartitionKey = "Locality",
            Description = "Parks in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "ParkName",
            Mappings = new List<PropertyOutput> { idProperty, park1, park2, park3, park4 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon
        };

        var parkStyleLayer = new StyleLayer(StyleType.fill);
        parkStyleLayer.AddPaintProperty("fill-color", "#ed800c");
        parkStyleLayer.AddPaintProperty("fill-outline-color", "#ed800c");
        parkStyleLayer.AddPaintProperty("fill-opacity", 0.8);
        parkStyleLayer.MinZoom = 11;

        var parkLayer = new FeatureLayer
        {
            Id = new Guid(),
            Name = "Parks",
            DisplayName = "Parks",
            CollectionName = "Parks",
            PartitionKey = "Locality",
            Description = "Parks in CC",
            Connection = conn,
            Mappings = new List<PropertyOutput> { idProperty, park1, park4 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon,
            Style = parkStyleLayer
        };

        var sta1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "StreetAddress",
            OutputName = "title",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };
        var sta2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "StreetAddressStatusDescription",
            OutputName = "current",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var sta3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "OccupationLevelDescription",
            OutputName = "occupationLevel",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var sta4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Locality",
            OutputName = "locality",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var sta5 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "geometry",
            PropertyType = PropertyType.Spatial,
            ShowLevel = ShowLevel.Public
        };

        var streetAddressFeature = new QueryConfig
        {
            Id = new Guid(),
            Name = "StreetAddresses",
            DisplayName = "Street Address",
            CollectionName = "StreetAddresses",
            PartitionKey = "Locality",
            Description = "Street Addresses in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "StreetAddress",
            Mappings = new List<PropertyOutput> { idProperty, sta1, sta2, sta3, sta4, sta5 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Point
        };


        var place1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "PlaceName",
            OutputName = "title",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };


        var place2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Locality",
            OutputName = "locality",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var place3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "geometry",
            PropertyType = PropertyType.Spatial,
            ShowLevel = ShowLevel.Public
        };

        var placeFeature = new QueryConfig
        {
            Id = new Guid(),
            Name = "Places",
            DisplayName = "PointOfInterest",
            CollectionName = "Places",
            PartitionKey = "Locality",
            Description = "Points of interest in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "PlaceName",
            Mappings = new List<PropertyOutput> { idProperty, place1, place2, place3 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Point
        };

        var ratingUnit1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "StreetAddress",
            OutputName = "title",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };


        var ratingUnit2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "OccupationLevelDescription",
            OutputName = "occupationLevel",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var ratingUnit3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Locality",
            OutputName = "locality",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var ratingUnit4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "geometry",
            PropertyType = PropertyType.Spatial,
            ShowLevel = ShowLevel.Public
        };

        var ratingUnit5 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ValuationVisitOrder",
            OutputName = "valuation",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var ratingUnit6 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ValuationRollNumber",
            OutputName = "valuationNumber",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };

        var ratingUnitFeature = new QueryConfig
        {
            Id = new Guid(),
            Name = "RatingUnits",
            DisplayName = "PointOfInterest",
            CollectionName = "RatingUnits",
            PartitionKey = "Locality",
            Description = "Rating Units in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "StreetAddress",
            Mappings = new List<PropertyOutput>
                { idProperty, ratingUnit1, ratingUnit2, ratingUnit3, ratingUnit4, ratingUnit5, ratingUnit6 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon
        };


        var ward1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "WardNameDescription",
            OutputName = "title",
            PropertyType = PropertyType.Normal,
            ShowLevel = ShowLevel.Public
        };


        var ward2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "geometry",
            PropertyType = PropertyType.Spatial,
            ShowLevel = ShowLevel.Public
        };

        var wardStyleLayer = new StyleLayer(StyleType.line);
        wardStyleLayer.AddPaintProperty("line-color", "#4086f7");
        wardStyleLayer.AddPaintProperty("line-width", 1.5);
        wardStyleLayer.AddPaintProperty("line-opacity", 0.8);
        wardStyleLayer.MinZoom = 10;

        var wardLayer = new FeatureLayer
        {
            Id = new Guid(),
            Name = "Wards",
            DisplayName = "Wards",
            CollectionName = "Wards",
            PartitionKey = "WardID",
            Description = "Wards in CC",
            Connection = conn,
            Mappings = new List<PropertyOutput> { idProperty, ward1, ward2 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon,
            Style = wardStyleLayer
        };

        defaultContext.Add(conn);
        defaultContext.Add(parkLayer);
        defaultContext.Add(wardLayer);

        defaultContext.Add(parkFeature);
        defaultContext.Add(placeFeature);
        defaultContext.Add(streetAddressFeature);
        defaultContext.Add(ratingUnitFeature);

        await defaultContext.SaveChangesAsync();
    }
}