namespace Test.Api.Middleware
{
    public class SetCorrelationIdMiddleware(RequestDelegate next)
    {
        public const string CORRELATION_ID = "Correlation-Id";
        const string _correlationIdHeader = "X-Correlation-Id";

        public async Task InvokeAsync(HttpContext context)
        {
            var id = Guid.NewGuid();
            if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var value)
                && value.FirstOrDefault() is var headerId
                && !string.IsNullOrEmpty(headerId))
            {
                if (Guid.TryParse(headerId, out id))
                {
                    throw new InvalidOperationException($"{_correlationIdHeader} does not contain a valid guid value");
                }
            }

            context.Items.Add(CORRELATION_ID, id);

            await next(context);
        }
    }
}
