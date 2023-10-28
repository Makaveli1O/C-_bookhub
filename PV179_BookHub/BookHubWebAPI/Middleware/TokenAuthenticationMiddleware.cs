namespace BookHubWebAPI.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenAuthenticationMiddleware> _logger;
        private const string HardcodedToken = "topsecret";

        public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(token) && token.Equals(HardcodedToken, StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
            }
            else
            {
                _logger.LogWarning($"Unauthorized access attempted: {context.Request.Method} {context.Request.Path}");
                context.Response.StatusCode = 401;
            }
        }
    }

}
