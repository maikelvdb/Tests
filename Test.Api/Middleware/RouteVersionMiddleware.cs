namespace Test.Api.Middleware
{
    public class RouteVersionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint() is not RouteEndpoint endpoint)
            {
                await next(context);
                return;
            }

            var routeData = context.Request.RouteValues;

            await next(context);
        }
    }
}
