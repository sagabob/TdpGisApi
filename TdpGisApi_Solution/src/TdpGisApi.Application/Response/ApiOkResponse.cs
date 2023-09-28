namespace TdpGisApi.Application.Response;

public class ApiOkResponse<TResult> : ApiBaseResponse
{
    public ApiOkResponse(TResult result)
        : base(true)
    {
        Result = result;
    }

    public TResult Result { get; set; }
}