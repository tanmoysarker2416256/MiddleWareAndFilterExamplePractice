using Microsoft.Extensions.Caching.Memory;

namespace MiddleWareExample.MiddleWare; 

public class IpRatLimiting
{
    private readonly RequestDelegate _next;
    private static readonly MemoryCache _cache = new(new MemoryCacheOptions());

    private const int Limit = 5;
    private static readonly TimeSpan window = TimeSpan.FromMinutes(1);

    public IpRatLimiting(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context)
    {
        string ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown" ;
        string key = $"ratelimit_{ip}";

        var counter = _cache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = window;
            return 0;
        });

        if (counter >= Limit)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsync("too many request in per miniute");
            return;
        }
       
        _cache.Set(key, counter+1);
        await _next(context);

    }


}

//requirment
// per ip  : limit to 5 request per min 