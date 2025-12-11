using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Odev09.WebApi.Filters
{
    public class ActionTimingFilter : IActionFilter
    {
        private Stopwatch _stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            Console.WriteLine($"Action '{context.ActionDescriptor.DisplayName}' başladı...");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            Console.WriteLine($"Action '{context.ActionDescriptor.DisplayName}' bitti. Geçen süre: {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}