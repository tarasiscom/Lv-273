using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace EPA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                Log.Information("Starting our web host.");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Error: {0}", ex);
            }
            finally
            {
                Log.Information("Logger Closing.");
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
