using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCensus.Worker;
using NetCensus.Worker.Services;
using Pastel;
using System.Drawing;

class Program
{
    static void Main(string[] args)
    {

        PrintWelcome();

        // Setup Host
        var host = CreateDefaultBuilder().Build();

        new ConfigureService().Check();


        // Invoke Worker
        using IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;
        var workerInstance = provider.GetRequiredService<Worker>();
        workerInstance.DoWork();

        host.Run();
    }
    
    static void PrintWelcome(){
        var appConfiguration = GetConfiguration();

        Console.WriteLine("@".Pastel(Color.Green) + "Net".Pastel(Color.White) + "Census".Pastel(Color.Green));
        Console.WriteLine($"Worker - Version: {appConfiguration["Version"]}".Pastel(Color.White));
        Console.WriteLine("---------------------------------------------------------------- \n");
    }

    static IConfiguration GetConfiguration() {
        return new ConfigurationBuilder()
           .AddEnvironmentVariables()
           .AddJsonFile("appsettings.json")
           .Build(); 
    }

    static IHostBuilder CreateDefaultBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(app =>
            {
                app.AddJsonFile("appsettings.json");
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<Worker>();
            });
    }
}