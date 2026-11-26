# ashkan-card-dnx

[![NuGet](https://img.shields.io/nuget/v/HamidRezaAshkiyan.svg)](https://www.nuget.org/packages/HamidRezaAshkiyan)
[![NuGet Downloads](https://img.shields.io/nuget/dt/HamidRezaAshkiyan.svg)](https://www.nuget.org/packages/HamidRezaAshkiyan)

A .NET global tool that displays HamidReza 'Ashkan' Ashkiyan's personal business card, portfolio, projects, engineering blog, and social profiles in your terminal. 💠

## Overview

A CLI tool built for the .NET ecosystem using the **`dnx`** command introduced in .NET 10! Run it instantly on any machine to access HamidReza 'Ashkan' Ashkiyan's business card, personal website, resume, featured projects, GitHub repositories, NuGet packages, blog posts, YouTube channel, and more—no installation required!

## Quick Start with dnx

The easiest way to run this tool on any computer with the .NET 10 SDK is with `dnx` (similar to `npx` in Node.js):

```bash
dnx hamidrezaashkiyan
```

That's it! The first time you run it, you'll be prompted to confirm the download. After that, it runs instantly without confirmation.

View help and available commands:

```bash
dnx hamidrezaashkiyan help
dnx hamidrezaashkiyan --help
```

Running `dnx hamidrezaashkiyan` with no arguments starts [interactive mode](#interactive-mode).

### Available Commands

Display the business card (also aliased as `ashkan`):

```bash
dnx hamidrezaashkiyan card
dnx hamidrezaashkiyan ashkan
```

Display featured engineering projects (also aliased as `work`):

```bash
dnx hamidrezaashkiyan projects
dnx hamidrezaashkiyan projects -c backend
dnx hamidrezaashkiyan projects -c systems
dnx hamidrezaashkiyan projects -c desktop
dnx hamidrezaashkiyan projects -c ai
```

Open HamidReza's personal website (aliases: `web`, `site`, `portfolio`):

```bash
dnx hamidrezaashkiyan website
```

Open HamidReza's printable resume (alias: `cv`):

```bash
dnx hamidrezaashkiyan resume
```

Open HamidReza's engineering blog:

```bash
dnx hamidrezaashkiyan blog
```

Open HamidReza's GitHub, LinkedIn, Telegram, YouTube (`@thisistechsin`), NuGet, or Contact page:

```bash
dnx hamidrezaashkiyan github
dnx hamidrezaashkiyan linkedin
dnx hamidrezaashkiyan telegram
dnx hamidrezaashkiyan youtube
dnx hamidrezaashkiyan nuget
dnx hamidrezaashkiyan contact
```

Display HamidReza's top GitHub repositories:

```bash
dnx hamidrezaashkiyan repos
```

Display HamidReza's NuGet packages:

```bash
dnx hamidrezaashkiyan packages
```

Display recent activity from Engineering Blog RSS, GitHub, and YouTube:

```bash
dnx hamidrezaashkiyan recent
dnx hamidrezaashkiyan recent --verbose
```

Display version and check for updates:

```bash
dnx hamidrezaashkiyan version
```

### Interactive Mode

Run in interactive mode to execute multiple commands without re-running the tool:

```bash
dnx hamidrezaashkiyan
# or
dnx hamidrezaashkiyan -i
```

In interactive mode, simply type commands. Type `help` to list them:

```text
ashkan> help
(lists available commands)

ashkan> card
(displays business card)

ashkan> projects -c systems
(displays Rust & systems projects)

ashkan> recent
(displays recent blog, GitHub, and YouTube activity)

ashkan> exit
```

Exit by typing `exit`, `quit`, or `q`. Built-in REPL commands also include `clear`/`cls`, `history`, and `clear-history`.

## Installation as a Global Tool

If you prefer to install the tool globally:

```bash
dotnet tool install -g HamidRezaAshkiyan
```

Then run from anywhere:

```bash
hamidrezaashkiyan            # Start interactive mode
hamidrezaashkiyan help       # List available commands
hamidrezaashkiyan card       # Display business card
hamidrezaashkiyan projects   # Display featured engineering projects
hamidrezaashkiyan website    # Open personal website
hamidrezaashkiyan blog       # Open engineering blog
hamidrezaashkiyan resume     # Open printable resume
```

To update:

```bash
dotnet tool update -g HamidRezaAshkiyan
```

To uninstall:

```bash
dotnet tool uninstall -g HamidRezaAshkiyan
```

## Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later (for `dnx` support)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
