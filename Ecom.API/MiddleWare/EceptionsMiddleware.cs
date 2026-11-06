using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.MiddleWare
{
    public class EceptionsMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _reteLimitWindow = TimeSpan.FromSeconds(30);
        public EceptionsMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplaySecurity(context);

                if (IsRequestAllowed(context) == false) {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests; //429
                    context.Response.ContentType = "application/json";

                    var response = new APIExeception((int)HttpStatusCode.TooManyRequests,
                        "Too Many Request . Please  Again Later");

                   await context.Response.WriteAsJsonAsync(response);
                }
              await  _next(context);
            }
            catch (Exception ex)
            {

              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // to get StatusCode

                context.Response.ContentType = "application/json"; // to make content like json data

                var reponse =_environment.IsDevelopment() ? 
                    new APIExeception((int)HttpStatusCode.InternalServerError,
                    ex.Message, ex.StackTrace
                    )
                    : new APIExeception((int)HttpStatusCode.InternalServerError,
                    ex.Message
                    );  // this object of messages 

                var Json = JsonSerializer.Serialize(reponse); // to convert the object or response to json

               await context.Response.WriteAsync(Json);
            }
        }
        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cashKey = $" Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timesTamp, count) = _memoryCache.GetOrCreate(cashKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _reteLimitWindow;
                return (timesTamp: dateNow, count: 0);
            });

            if (dateNow - timesTamp < _reteLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cashKey, (timesTamp, count+=1), _reteLimitWindow);
            }
            else
            {
                _memoryCache.Set(cashKey, (timesTamp, count), _reteLimitWindow);
            }
            return true;
        }

        private void ApplaySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";

            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";

            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
