# ashkan-card-dnx

[![NuGet](https://img.shields.io/nuget/v/HamidRezaAshkiyan.svg)](https://www.nuget.org/packages/HamidRezaAshkiyan)
[![NuGet Downloads](https://img.shields.io/nuget/dt/HamidRezaAshkiyan.svg)](https://www.nuget.org/packages/HamidRezaAshkiyan)

A .NET global tool that displays HamidReza 'Ashkan' Ashkiyan's personal business card in your terminal. 💠

## Overview

A CLI tool built for the .NET ecosystem using the **`dnx`** command introduced in .NET 10! Run it instantly to access HamidReza 'Ashkan' Ashkiyan's profiles, repositories, NuGet packages, YouTube channel, and more—no installation required!

## Quick Start with dnx

The easiest way to run this tool is with the new `dnx` command (similar to `npx` in Node.js):

```bash
dnx hamidrezaashkiyan
```

That's it! The first time you run it, you'll be prompted to confirm the download. After that, it runs instantly without confirmation.

### Available Commands

Start interactive REPL mode (`ashkan>`):

```bash
dnx hamidrezaashkiyan
```

Display the business card (also aliased as `ashkan`):

```bash
dnx hamidrezaashkiyan card
dnx hamidrezaashkiyan ashkan
```

Open HamidReza's GitHub profile:

```bash
dnx hamidrezaashkiyan github
```

Open HamidReza's LinkedIn profile:

```bash
dnx hamidrezaashkiyan linkedin
```

Open HamidReza's Telegram profile:

```bash
dnx hamidrezaashkiyan telegram
```

Open HamidReza's YouTube channel (`@thisistechsin`):

```bash
dnx hamidrezaashkiyan youtube
```

Open HamidReza's NuGet profile:

```bash
dnx hamidrezaashkiyan nuget
```

Open HamidReza's blog:

```bash
dnx hamidrezaashkiyan blog
```

Contact HamidReza (opens LinkedIn):

```bash
dnx hamidrezaashkiyan contact
```

Display HamidReza's GitHub repositories:

```bash
dnx hamidrezaashkiyan repos
```

Display HamidReza's NuGet packages:

```bash
dnx hamidrezaashkiyan packages
```

Display recent activity from GitHub and YouTube:

```bash
dnx hamidrezaashkiyan recent
dnx hamidrezaashkiyan recent --verbose
```

Display version and check for updates:

```bash
dnx hamidrezaashkiyan version
```

## Installation as a Global Tool

If you prefer to install the tool globally:

```bash
dotnet tool install -g HamidRezaAshkiyan
```

Then run:

```bash
hamidrezaashkiyan card
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
