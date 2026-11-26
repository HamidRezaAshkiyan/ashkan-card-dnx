# AGENTS.md — AshkanCardDnx (`HamidRezaAshkiyan.Cli`)

## Overview
A .NET 10 global CLI tool (published to NuGet as `HamidRezaAshkiyan`, command name `hamidrezaashkiyan`) designed for instant execution via `dnx hamidrezaashkiyan`. It renders HamidReza 'Ashkan' Ashkiyan's terminal business card, opens social/profile links with UTM tracking, queries GitHub repos, NuGet packages, recent GitHub/YouTube RSS activity, and supports an interactive REPL (`ashkan>`).

## Tech Stack & Requirements
- **Language / Runtime:** C# on **.NET 10.0 SDK** (`net10.0`, `<RollForward>Major</RollForward>`).
- **CLI Framework:** `TimeWarp.Nuru` (`3.0.0-beta.68`) with source-generated interceptors (`<InterceptorsNamespaces>TimeWarp.Nuru.Generated</InterceptorsNamespaces>`) and `TimeWarp.Terminal` widgets.
- **DI & HTTP:** `Microsoft.Extensions.Hosting` (`10.0.5`), `Microsoft.Extensions.Http` (`10.0.5`).
- **Testing:** `TUnit` running on **Microsoft.Testing.Platform** (configured in `global.json`) with `Microsoft.Testing.Extensions.CodeCoverage`.
- **Solution Format:** Modern XML solution `hamidrezaashkiyan.slnx`.

## Repository Structure
```text
AshkanCardDnx/
├── hamidrezaashkiyan.slnx          # Solution file (src + tests)
├── global.json                     # Configures Microsoft.Testing.Platform runner
├── coverage.settings               # Code coverage filters
├── testcoverage.cs                 # dotnet-script coverage helper (see Gotchas)
├── src/HamidRezaAshkiyan.Cli/
│   ├── HamidRezaAshkiyan.Cli.csproj# Tool packaging & NuGet metadata
│   ├── Program.cs                  # Entry point, NuruApp builder, DI, REPL setup
│   ├── Urls.cs                     # Canonical profile/social URLs
│   ├── NuGetVersionData.cs         # Version lookup models
│   ├── Helpers/UrlHelper.cs        # UTM query-string & browser-open helpers
│   └── Endpoints/                  # Auto-discovered [NuruRoute] endpoints
│       ├── CardEndpoint.cs         # `card` / alias `ashkan`
│       ├── HelpEndpoint.cs         # `help` (lists commands, aliases & REPL built-ins)
│       ├── ProjectsEndpoint.cs     # `projects [--category]` / alias `work`
│       ├── UrlEndpoints.cs         # `website`, `resume`, `blog`, `contact`, `github`, `linkedin`, `telegram`, `youtube`, `nuget`
│       ├── ReposEndpoint.cs        # `repos` (GitHub API)
│       ├── PackagesEndpoint.cs     # `packages` (NuGet search API)
│       ├── RecentEndpoint.cs       # `recent [--verbose]` (Blog RSS + GitHub + YouTube)
│       └── VersionEndpoint.cs      # `version` (NuGet update check)
└── tests/HamidRezaAshkiyan.Cli.Tests/
    ├── HamidRezaAshkiyan.Cli.Tests.csproj
    ├── FakeHttpClientFactory.cs    # HTTP test double
    ├── TestHelpers.cs
    └── Endpoints/ & Helpers/       # TUnit test suites
```

## Architecture & Key Flows
- **Entry Point:** `src/HamidRezaAshkiyan.Cli/Program.cs` sets UTF-8 console output, builds `NuruApp` with `UseMicrosoftDependencyInjection()`, registers three named `HttpClient` instances (`GitHub`, `NuGet`, `RssFeed`, each with 10s timeout), calls `.DiscoverEndpoints()`, and enables `.AddRepl(...)` with `AutoStartWhenEmpty = true`.
- **Invocation Modes:**
  - No args or `-i` / `--interactive`: starts the `ashkan>` REPL.
  - Subcommand args (e.g., `card`, `repos`, `packages`, `recent --verbose`, `version`, or URL commands): dispatched to the matching `[NuruRoute]` / `[NuruRouteAlias]` endpoint via `TimeWarp.Nuru` source-generated interceptors.
- **Endpoint Pattern:** Each endpoint implements `IQuery<Unit>` (or command interface) with a nested `Handler : IQueryHandler<..., Unit>` that resolves `IHttpClientFactory` via DI and writes styled output through `TimeWarpTerminal.Default`.

## Development & Commands
```bash
# Restore & build solution
dotnet build hamidrezaashkiyan.slnx

# Run CLI directly (pass args after --)
dotnet run --project src/HamidRezaAshkiyan.Cli -- card
dotnet run --project src/HamidRezaAshkiyan.Cli -- recent --verbose
dotnet run --project src/HamidRezaAshkiyan.Cli -- -i

# Run tests (Microsoft.Testing.Platform via global.json)
dotnet test hamidrezaashkiyan.slnx

# Pack as .NET global/dnx tool
dotnet pack src/HamidRezaAshkiyan.Cli/HamidRezaAshkiyan.Cli.csproj -c Release

# Publish to NuGet (manual; requires NuGet API key)
dotnet nuget push src/HamidRezaAshkiyan.Cli/bin/Release/HamidRezaAshkiyan.<version>.nupkg \
  --api-key <NUGET_API_KEY> --source https://api.nuget.org/v3/index.json
```

## Configuration & Environment
- No runtime `.env` or secret configuration is required; all external calls hit public GitHub, NuGet, and RSS endpoints.
- `CI=true`: checked by `testcoverage.cs` to skip local `reportgenerator` tool installation and browser launch.
- Publishing requires a NuGet API key supplied to `dotnet nuget push` (or CI secret).

## Conventions & Gotchas
- **Source-generated interceptors:** `<InterceptorsNamespaces>` in `HamidRezaAshkiyan.Cli.csproj` is required for `TimeWarp.Nuru` route discovery (`DiscoverEndpoints()`); do not remove it or downgrade `TimeWarp.Nuru` carelessly.
- **MTP + TUnit:** Because `global.json` sets `"runner": "Microsoft.Testing.Platform"` and the test project has `<OutputType>Exe</OutputType>`, test CLI flags follow MTP conventions rather than legacy VSTest flags.
- **Coverage script (`testcoverage.cs`):** Targets `tests/HamidRezaAshkiyan.Cli.Tests`, searches both `tests/` and `TestResults/` for `coverage.cobertura.xml`, and opens the HTML report cross-platform when `CI` is not `true`.
- **Versioning:** Bump `<Version>` and `<ReleaseNotes>` in `src/HamidRezaAshkiyan.Cli/HamidRezaAshkiyan.Cli.csproj` before packing a release.
