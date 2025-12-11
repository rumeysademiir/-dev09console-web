using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Odev09.WebApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("=== REQUEST ===");
            Console.WriteLine($"Method: {context.Request.Method}");
            Console.WriteLine($"Path  : {context.Request.Path}");
            Console.WriteLine($"Time  : {DateTime.Now}");

            var stopwatch = Stopwatch.StartNew();

            await _next(context); 

            stopwatch.Stop();

            Console.WriteLine("=== RESPONSE ===");
            Console.WriteLine($"Status Code: {context.Response.StatusCode}");
            Console.WriteLine($"Elapsed    : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("======================\n");
        }
    }
}