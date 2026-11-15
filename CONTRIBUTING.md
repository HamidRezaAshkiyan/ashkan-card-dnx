# Contributing to ashkan-card-dnx

## For Maintainers

### Publishing a New Version

This project can be published to NuGet.org via GitHub Actions or manually.

#### Steps to Publish

1. **Update the version and release notes** in `src/HamidRezaAshkiyan.Cli/HamidRezaAshkiyan.Cli.csproj`:

   ```xml
   <Version>1.0.0</Version>
   <ReleaseNotes>Initial release for HamidReza Ashkiyan CLI business card.</ReleaseNotes>
   ```

2. **Update `README.md`** if needed to document new features.

3. **Commit and push** your changes:

   ```bash
   git add .
   git commit -m "Bump version to 1.0.0"
   git push origin main
   ```

4. **Manual Publishing**:

   ```bash
   dotnet pack src/HamidRezaAshkiyan.Cli/HamidRezaAshkiyan.Cli.csproj -c Release
   dotnet nuget push src/HamidRezaAshkiyan.Cli/bin/Release/HamidRezaAshkiyan.1.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
   ```

## For Contributors

Contributions are welcome! Feel free to:

- Report issues
- Suggest improvements
- Submit pull requests

Please ensure `dotnet test hamidrezaashkiyan.slnx` passes before submitting changes.
