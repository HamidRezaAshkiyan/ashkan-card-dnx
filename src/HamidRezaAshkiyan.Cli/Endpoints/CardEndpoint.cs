using System.Threading;
using System.Threading.Tasks;
using TimeWarp.Nuru;
using TimeWarp.Terminal;
using static HamidRezaAshkiyan.Cli.Urls;
using static HamidRezaAshkiyan.Cli.Helpers.UrlHelper;

namespace HamidRezaAshkiyan.Cli.Endpoints;

/// <summary>
/// Displays HamidReza 'Ashkan' Ashkiyan's business card using Nuru terminal widgets.
/// </summary>
[NuruRoute("card", Description = "Display HamidReza 'Ashkan' Ashkiyan's business card")]
[NuruRouteAlias("ashkan")]
public sealed class CardEndpoint : IQuery<Unit>
{
  public sealed class Handler : IQueryHandler<CardEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(CardEndpoint query, CancellationToken ct)
    {
      ITerminal terminal = TimeWarpTerminal.Default;

      // URLs with UTM tracking
      string websiteUrl = AddUtmSource(Website);
      string blogUrl = AddUtmSource(Blog);
      string resumeUrl = AddUtmSource(Resume);
      string githubUrl = AddUtmSource(GitHub);
      string linkedinUrl = AddUtmSource(LinkedIn);
      string telegramUrl = AddUtmSource(Telegram);
      string youtubeUrl = AddUtmSource(YouTube);
      string nugetUrl = AddUtmSource(NuGet);

      // Top rule
      terminal.WriteRule(rule => rule.Color(AnsiColors.Cyan));

      // Card panel
      terminal.WritePanel(panel => panel
        .Header("💠 Ashkan".Cyan().Bold())
        .Content(
          "HamidReza 'Ashkan' Ashkiyan".Magenta().Bold() + "\n" +
          "Software Engineer · Deka (Entekhab Group)".White() + "\n" +
          "ASP.NET 10 Microservices & Distributed Systems · Isfahan, Iran".Gray() + "\n\n" +
          Website.Link(websiteUrl).Cyan() + " • " +
          "Blog".Link(blogUrl).Cyan() + " • " +
          "Resume".Link(resumeUrl).Cyan() + "\n" +
          "GitHub".Link(githubUrl).Cyan() + " • " +
          "LinkedIn".Link(linkedinUrl).Cyan() + " • " +
          "Telegram".Link(telegramUrl).Cyan() + " • " +
          "YouTube".Link(youtubeUrl).Cyan() + " • " +
          "NuGet".Link(nugetUrl).Cyan() + "\n\n" +
          ".NET 10 • Microservices • Clean Architecture • DDD • Rust • Swift".Gray().Italic())
        .Border(BorderStyle.Rounded)
        .BorderColor(AnsiColors.Magenta)
        .Padding(2, 1));

      // Bottom rule
      terminal.WriteRule(rule => rule.Color(AnsiColors.Magenta));

      terminal.WriteLine();
      terminal.WriteLine(
        "Try '".Gray() +
        "hamidrezaashkiyan projects".Cyan() +
        "', '".Gray() +
        "hamidrezaashkiyan repos".Cyan() +
        "', or '".Gray() +
        "hamidrezaashkiyan help".Magenta() +
        "' for more options".Gray());

      return default;
    }
  }
}
