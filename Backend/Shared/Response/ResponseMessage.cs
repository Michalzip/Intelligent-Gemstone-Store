using System.Net;
using Newtonsoft.Json;

namespace Shared;

public class ResponseMessage
{
    public HttpStatusCode code { get; set; }
    public string Message { get; set; }

    public ResponseMessage(HttpStatusCode codeStatus, string message)
    {
        code = codeStatus;
        Message = message;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
