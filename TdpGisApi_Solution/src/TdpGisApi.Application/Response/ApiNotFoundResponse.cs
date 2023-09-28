namespace TdpGisApi.Application.Response;

public abstract class ApiNotFoundResponse : ApiBaseResponse
{
    protected ApiNotFoundResponse(string message)
        : base(false)
    {
        Message = message;
    }

    public string Message { get; set; }
}