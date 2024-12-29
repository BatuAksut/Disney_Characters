using Serilog;

namespace Disney_Characters.Extensions;

public static class SerilogExtenstion
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        host.UseSerilog((_, _, configuration) => 
            configuration.WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));
    }
}