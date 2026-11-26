#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeWarp.Nuru;
using TimeWarp.Terminal;
using static HamidRezaAshkiyan.Cli.Urls;
using static HamidRezaAshkiyan.Cli.Helpers.UrlHelper;

namespace HamidRezaAshkiyan.Cli.Endpoints;

/// <summary>
/// Displays HamidReza 'Ashkan' Ashkiyan's featured engineering projects from his portfolio and repositories.
/// </summary>
[NuruRoute("projects", Description = "Display HamidReza's featured engineering projects")]
[NuruRouteAlias("work")]
public sealed class ProjectsEndpoint : IQuery<Unit>
{
  [Option("category", "c", Description = "Filter by category: backend, systems, desktop, ai")]
  public string? Category { get; set; }

  public sealed record ProjectInfo(
    string Name,
    string Category,
    string Stack,
    string Description,
    string? Url);

  private static readonly IReadOnlyList<ProjectInfo> AllProjects =
  [
    // Backend & .NET
    new("DekaSeir", "backend", "ASP.NET 10, gRPC, Kafka", "Distributed microservices platform at Deka (Ocelot Gateway, CQRS, Redis)", null),
    new("ModularMonolith", "backend", "C#, .NET 10, MediatR", "Reference 4-module Modular Monolith with Clean Arch, DDD & event bus", $"{GitHub}/ModularMonolith"),
    new("mrapi_api", "backend", "C#, .NET 10, Postgres", "7-module multi-tenant API for OTP, messaging, wallet billing & loyalty", $"{GitHub}/mrapi_api"),
    new("BackEndCodeChallenge", "backend", ".NET 10, RabbitMQ", "Event-driven Order Management microservices with MassTransit & Keycloak", $"{GitHub}/BackEndCodeChallenge"),

    // Systems & Rust
    new("aegis", "systems", "Rust, DNS, Proxy", "System-wide ad, tracker & OS telemetry blocker with VPN chaining", $"{GitHub}/aegis"),
    new("ophiuchi-rs", "systems", "Rust, Tauri v2, React", "Cross-platform localhost SSL reverse proxy with automated Local Root CA", $"{GitHub}/ophiuchi-rs"),
    new("folio", "systems", "Rust, Ratatui, Iced", "Cross-platform Bullet Journal (TUI, GUI, Web, Obsidian) over Markdown", $"{GitHub}/folio"),
    new("clop-rs", "systems", "Rust, FFmpeg, TUI", "Automatic clipboard, image, video & PDF optimizer with floating HUD", $"{GitHub}/clop-rs"),
    new("tg-tui", "systems", "Rust, MTProto, SQLite", "Pure-Rust TUI Telegram client with multi-account sessions & previews", $"{GitHub}/tg-tui"),
    new("umbra", "systems", "Rust, wgpu, WGSL", "Ultra-minimal GPU WGSL animated dark wallpaper engine", $"{GitHub}/umbra"),

    // Desktop, Mobile & Tools
    new("Hafez", "desktop", "C#, .NET 10, WPF", "Flagship mosque LED/projector multimedia & hardware control system", $"{GitHub}/Hafez"),
    new("PadDisplay", "desktop", "Swift 6, C#", "Turn an iPad into a second monitor over Wi-Fi/USB with no iOS app", $"{GitHub}/PadDisplay"),
    new("PersianKeyboardRedefined", "desktop", "MSKLC, macOS, XKB", "Ergonomic 'Persian but Better' keyboard layout across all OSes", $"{GitHub}/PersianKeyboardRedefined"),
    new("ashkan-card-dnx", "desktop", "C#, .NET 10, Nuru", "This .NET 10 dnx CLI business card & interactive REPL", $"{GitHub}/ashkan-card-dnx"),
    new("pomo-pet", "desktop", "Swift 6, WinUI 3", "Dual-native Pomodoro companion with shared 16x16 pixel-art pets", $"{GitHub}/pomo-pet"),

    // AI & Obsidian
    new("agent-bridge", "ai", "Python, Bash, pwsh", "Zero-dependency coding agent installer, provider config & MCP bridge", $"{GitHub}/agent-bridge"),
    new("agy-telegram-bot", "ai", "Python, Telegram", "Security-first Telegram bridge to control local Antigravity agents", $"{GitHub}/agy-telegram-bot"),
    new("obsidian-persian-calendar", "ai", "TypeScript, Obsidian", "Obsidian plugin for Solar Hijri (Jalali), Gregorian & Lunar calendars", $"{GitHub}/obsidian-persian-calendar"),
    new("obsidian-notion-sync", "ai", "TypeScript, Node.js", "Hybrid Obsidian plugin & CLI for Notion publishing and vault backups", $"{GitHub}/obsidian-notion-sync"),
  ];

  public static IReadOnlyList<ProjectInfo> GetProjects(string? category = null)
  {
    if (string.IsNullOrWhiteSpace(category))
    {
      return AllProjects;
    }

    string normalized = category.Trim();
    return [.. AllProjects.Where(p => p.Category.Equals(normalized, StringComparison.OrdinalIgnoreCase))];
  }

  public static void Render(ITerminal terminal, string? category = null)
  {
    IReadOnlyList<ProjectInfo> projects = GetProjects(category);

    if (projects.Count == 0)
    {
      terminal.WriteLine($"No projects found for category '{category}'.".Yellow());
      terminal.WriteLine("Available categories: backend, systems, desktop, ai".Gray());
      return;
    }

    string heading = string.IsNullOrWhiteSpace(category)
      ? "HamidReza's Featured Engineering Projects"
      : $"HamidReza's Projects ({category.Trim().ToLowerInvariant()})";

    terminal.WriteLine(heading.Green().Bold());
    terminal.WriteLine();

    terminal.WriteTable(table =>
    {
      table
        .AddColumn("Project")
        .AddColumn("Stack")
        .AddColumn("Description")
        .Border(BorderStyle.Rounded);

      foreach (ProjectInfo project in projects)
      {
        string nameCell = project.Url is not null
          ? project.Name.Link(AddUtmSource(project.Url)).Cyan()
          : project.Name.Cyan();

        table.AddRow(nameCell, project.Stack.Yellow(), project.Description.Gray());
      }
    });

    terminal.WriteLine();
    terminal.WriteLine(
      "Portfolio: ".Gray() +
      Website.Link(AddUtmSource(Website)).Cyan() +
      "  •  ".Gray() +
      "Filter with: ".Gray() +
      "projects -c backend|systems|desktop|ai".Magenta());
  }

  public sealed class Handler : IQueryHandler<ProjectsEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(ProjectsEndpoint query, CancellationToken ct)
    {
      Render(TimeWarpTerminal.Default, query.Category);
      return default;
    }
  }
}
