using System.Diagnostics;

namespace WebApp.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
            {
            _logger = logger;
            _stopwatch = new Stopwatch();
            }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var ellapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            if (ellapsedMilliseconds / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {ellapsedMilliseconds} ms";

                _logger.LogInformation(message);
            }
        }
    }
}
