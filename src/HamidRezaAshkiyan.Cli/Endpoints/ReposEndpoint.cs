#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TimeWarp.Nuru;
using TimeWarp.Terminal;
using static HamidRezaAshkiyan.Cli.Urls;

namespace HamidRezaAshkiyan.Cli.Endpoints;

/// <summary>
/// Displays HamidReza's popular GitHub repositories using Nuru table widget.
/// </summary>
[NuruRoute("repos", Description = "Display HamidReza's GitHub repositories")]
public sealed class ReposEndpoint : IQuery<Unit>
{
    public sealed class Handler(
        IHttpClientFactory httpClientFactory) : IQueryHandler<ReposEndpoint, Unit>
    {
        public async ValueTask<Unit> Handle(ReposEndpoint query, CancellationToken ct)
        {
            ITerminal terminal = TimeWarpTerminal.Default;

            terminal.WriteLine("HamidReza's GitHub Repositories".Green().Bold());
            terminal.WriteLine();

            HttpClient client = httpClientFactory.CreateClient("GitHub");
            List<GitHubRepo> repos = [];

            try
            {
                HttpResponseMessage response = await client.GetAsync(
                    "users/hamidrezaashkiyan/repos?sort=updated&per_page=10", ct);
                response.EnsureSuccessStatusCode();

                List<GitHubRepo>? fetched = await response.Content
                    .ReadFromJsonAsync<List<GitHubRepo>>(ct);

                if (fetched != null)
                {
                    repos = fetched
                        .OrderByDescending(r => r.StargazersCount)
                        .ThenByDescending(r => r.Name)
                        .Take(10)
                        .ToList();
                }
            }
            catch
            {
                // Handled below when repos.Count == 0
            }

            if (repos.Count == 0)
            {
                terminal.WriteLine("Unable to load GitHub repositories right now.".Yellow());
                terminal.WriteLine("Visit: ".Gray() + GitHub.Link(GitHub).Cyan());
                return default;
            }

            DisplayTable(repos);

            return default;
        }

        private static void DisplayTable(List<GitHubRepo> repos)
        {
            var terminal = TimeWarpTerminal.Default;

            terminal.WriteTable(table =>
            {
                table
                    .AddColumn("Repository")
                    .AddColumn("Stars", Alignment.Right)
                    .AddColumn("Description")
                    .Border(BorderStyle.Rounded);

                foreach (GitHubRepo repoInfo in repos)
                {
                    string description = repoInfo.Description ?? "No description";
                    if (description.Length > 60)
                        description = description[..57] + "...";

                    table.AddRow(
                        repoInfo.Name.Link(repoInfo.HtmlUrl).Cyan(),
                        $"\u2b50 {repoInfo.StargazersCount:N0}".Yellow(),
                        description.Gray()
                    );
                }
            });

            terminal.WriteLine();
            terminal.WriteLine("Visit: ".Gray() + GitHub.Link(GitHub).Cyan());
        }
    }

    private sealed class GitHubRepo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; } = string.Empty;
    }
}
