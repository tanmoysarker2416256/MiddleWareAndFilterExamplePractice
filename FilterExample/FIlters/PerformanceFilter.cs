using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace FilterExample.FIlters;

public class PerformanceFilter : IActionFilter
{

    private Stopwatch _timer;
    public void OnActionExecuted(ActionExecutedContext context)
    {

        _timer.Stop();
        var elapsed = _timer.ElapsedMilliseconds;
        Console.WriteLine($"Action executed in {elapsed} ms ");
       
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _timer = Stopwatch.StartNew();
    }
}
