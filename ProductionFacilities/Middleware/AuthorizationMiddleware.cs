using Microsoft.AspNetCore.Authentication.OAuth;

namespace ProductionFacilities.WebApi.Middleware
{
    public class AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key missing");
                return;
            }

            if (!extractedApiKey.Equals(configuration.GetValue<string>(AuthConstants.ApiKeySectionName)))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await next(context);
        }
    }
}
