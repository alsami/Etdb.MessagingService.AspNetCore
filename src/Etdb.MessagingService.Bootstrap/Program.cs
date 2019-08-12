using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Etdb.MessagingService.Bootstrap
{
    public class Program
    {
        private static readonly string LogPath = Path.Combine(AppContext.BaseDirectory, "Logs",
            $"{Assembly.GetExecutingAssembly().GetName().Name}.log");
        
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureLogging(ConfigureLogging)
                        .UseSerilog();
                });

        private static void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder _)
        {
            var environment = context.HostingEnvironment;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(environment.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile(Program.LogPath)
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
        }
    }
}