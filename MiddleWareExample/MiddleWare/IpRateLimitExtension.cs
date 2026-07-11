namespace MiddleWareExample.MiddleWare; 

public static class IpRateLimitExtension
{
    public static IApplicationBuilder UseIpRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IpRatLimiting>();
    }

}
