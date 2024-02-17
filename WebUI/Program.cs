using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile(path:"appsettings.json",optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args!=null)
                    {
                        config.AddCommandLine(args);
                    }   //AppSeting.json üzerinde veri deðiþtiðinde runtime direk yansýmasý.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(log =>
                {
                    log.ClearProviders();
                }).UseNLog();  //Nlog implement
    }
}
