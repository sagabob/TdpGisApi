namespace TdpGisApi.Application.Response;

public class ApiOkResponse<TResult> : ApiBaseResponse
{
    public ApiOkResponse(TResult result)
        : base(true)
    {
        Results = result;
    }

    public TResult Results { get; set; }
}