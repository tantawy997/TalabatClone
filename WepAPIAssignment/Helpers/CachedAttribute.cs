using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using System.Text;

namespace WepAPIAssignment.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSeconds;

        public CachedAttribute(int _TimeToLiveInSeconds)
        {
            timeToLiveInSeconds = _TimeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachedSerivce = context.HttpContext.RequestServices.GetRequiredService<IResponseCache>();

            var CachedKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var CachedResponse = await CachedSerivce.GetCacheResponse(CachedKey);

            if (!string.IsNullOrWhiteSpace(CachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = CachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var ExecutedContext = await next();

            if(ExecutedContext.Result is OkObjectResult objectResult)
            {
                await CachedSerivce.CacheResponseAsync(CachedKey, objectResult.Value, TimeSpan.FromSeconds(timeToLiveInSeconds));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder KeyBuilder = new StringBuilder();

            KeyBuilder.Append(request.Path);
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");

            }

            return KeyBuilder.ToString();
        }
    }
}
