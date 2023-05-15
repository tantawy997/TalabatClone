using System.Net;
using System.Text.Json;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.MiddleWirres
{
    public class ExpectionMiddlwirres
    {
        private readonly ILogger<ExpectionMiddlwirres> ilooger;
        private readonly RequestDelegate next;
        private readonly IHostEnvironment hostEnvironment;

        public ExpectionMiddlwirres(ILogger<ExpectionMiddlwirres> _ilooger,
            RequestDelegate _next, IHostEnvironment _hostEnvironment)

        {
            ilooger = _ilooger;
            next = _next;
            hostEnvironment = _hostEnvironment;
        }

        public async Task InvokeAsynce(HttpContext httpContext)
        {
            try
            {
               await next(httpContext);
            }
            catch (Exception ex)
            {

                ilooger.LogError(ex,ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = hostEnvironment.IsDevelopment() ? new ApiExpection((int)HttpStatusCode.InternalServerError
                    , ex.Message, ex.StackTrace.ToString()) : new ApiExpection((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}
