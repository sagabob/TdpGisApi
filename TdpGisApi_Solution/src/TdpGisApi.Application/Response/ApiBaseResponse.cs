namespace TdpGisApi.Application.Response;

public abstract class ApiBaseResponse
{
    protected ApiBaseResponse(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}