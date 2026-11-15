using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Nuru;

namespace HamidRezaAshkiyan.Cli;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        NuruApp app = NuruApp.CreateBuilder()
            .UseMicrosoftDependencyInjection()
            .ConfigureServices(services =>
            {
                services.AddLogging();

                // Named HTTP clients for DI
                services.AddHttpClient("GitHub", client =>
                {
                    client.BaseAddress = new Uri("https://api.github.com/");
                    client.DefaultRequestHeaders.Add("User-Agent", "hamidrezaashkiyan-cli");
                    client.Timeout = TimeSpan.FromSeconds(10);
                });

                services.AddHttpClient("NuGet", client =>
                {
                    client.BaseAddress = new Uri("https://api-v2v3search-0.nuget.org/");
                    client.Timeout = TimeSpan.FromSeconds(10);
                });

                services.AddHttpClient("RssFeed", client =>
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "hamidrezaashkiyan-cli");
                    client.Timeout = TimeSpan.FromSeconds(10);
                });
            })
            // Auto-discover all [NuruRoute] endpoints
            .DiscoverEndpoints()
            // Configure REPL mode
            .AddRepl(options =>
            {
                options.Prompt = "ashkan> ";
                options.WelcomeMessage =
                    "Welcome to HamidReza 'Ashkan' Ashkiyan CLI Interactive Mode!\n" +
                    "Type 'help' for available commands, or 'exit' to quit.";
                options.AutoStartWhenEmpty = true;
            })
            .Build();

        // Check for interactive mode flag
        if (args.Length > 0 && (args[0] == "-i" || args[0] == "--interactive"))
        {
            await app.RunReplAsync();
            return default;
        }

        await app.RunAsync(args);
        return default;
    }
}
