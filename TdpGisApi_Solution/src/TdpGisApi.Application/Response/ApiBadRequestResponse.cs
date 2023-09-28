namespace TdpGisApi.Application.Response;

public abstract class ApiBadRequestResponse : ApiBaseResponse
{
    protected ApiBadRequestResponse(string message)
        : base(false)
    {
        Message = message;
    }

    public string Message { get; set; }
}