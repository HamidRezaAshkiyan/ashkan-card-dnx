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
                    "Software Engineer".Gray() + "\n\n" +
                    GitHub.Link(githubUrl).Cyan() + "\n\n" +
                    "LinkedIn".Link(linkedinUrl).Cyan() + " • " +
                    "Telegram".Link(telegramUrl).Cyan() + " • " +
                    "YouTube".Link(youtubeUrl).Cyan() + " • " +
                    "NuGet".Link(nugetUrl).Cyan() + "\n\n" +
                    ".NET • C# • Software Architecture".Gray().Italic())
                .Border(BorderStyle.Rounded)
                .BorderColor(AnsiColors.Magenta)
                .Padding(2, 1));

            // Bottom rule
            terminal.WriteRule(rule => rule.Color(AnsiColors.Magenta));

            terminal.WriteLine();
            terminal.WriteLine(
                "Try '".Gray() +
                "hamidrezaashkiyan repos".Cyan() +
                "' or '".Gray() +
                "hamidrezaashkiyan youtube".Magenta() +
                "' for more options".Gray());

            return default;
        }
    }
}
