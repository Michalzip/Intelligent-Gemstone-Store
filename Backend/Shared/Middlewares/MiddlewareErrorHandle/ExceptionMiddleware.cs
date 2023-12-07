using System.Net;
using Newtonsoft.Json;
using Shared.Common;
using Shared.Middlewares.MiddlewareErrorHandle.Abstract;
using Shared.ValueObjects.Common;

namespace Shared.Middlewares.MiddlewareErrorHandle;

public class ExceptionMiddleware : AbstractExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate next)
        : base(next) { }

    public override string GetResponse(Exception exception)
    {
        HttpStatusCode code;

        switch (exception)
        {
            case UnauthorizedException:
                code = HttpStatusCode.Unauthorized;
                break;
            case InvalidValueException:
                code = HttpStatusCode.NotAcceptable;
                break;
            case InvalidCredentialsException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return new ResponseMessage(code, exception.Message).ToJson();
    }
}
