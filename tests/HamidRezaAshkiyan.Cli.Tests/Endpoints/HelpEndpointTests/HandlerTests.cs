using HamidRezaAshkiyan.Cli.Endpoints;
using TimeWarp.Terminal;

namespace HamidRezaAshkiyan.Cli.Tests.Endpoints.HelpEndpointTests;

/// <summary>
/// Tests for HelpEndpoint, which gives interactive mode a working "help" command.
/// </summary>
public class HandlerTests
{
  [Test]
  public async Task GetCommands_IncludesEveryRoutedEndpoint()
  {
    var names = HelpEndpoint.GetCommands().Select(c => c.Name).ToList();

    await Assert.That(names).Contains("card");
    await Assert.That(names).Contains("version");
    await Assert.That(names).Contains("projects");
    await Assert.That(names).Contains("website");
    await Assert.That(names).Contains("resume");
    await Assert.That(names).Contains("blog");
    await Assert.That(names).Contains("help");
  }

  [Test]
  public async Task GetCommands_IncludesDescriptions()
  {
    var card = HelpEndpoint.GetCommands().Single(c => c.Name == "card");

    await Assert.That(card.Description).IsEqualTo("Display HamidReza 'Ashkan' Ashkiyan's business card");
  }

  [Test]
  public async Task GetCommands_IncludesAliases()
  {
    var card = HelpEndpoint.GetCommands().Single(c => c.Name == "card");
    var projects = HelpEndpoint.GetCommands().Single(c => c.Name == "projects");

    await Assert.That(card.Aliases).Contains("ashkan");
    await Assert.That(projects.Aliases).Contains("work");
  }

  [Test]
  public async Task GetCommands_IsSortedByName()
  {
    var names = HelpEndpoint.GetCommands().Select(c => c.Name).ToList();
    var sorted = names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();

    await Assert.That(names).IsEquivalentTo(sorted, TUnit.Assertions.Enums.CollectionOrdering.Matching);
  }

  [Test]
  public async Task Render_WritesCommandsAndInteractiveBuiltIns()
  {
    using var terminal = new TestTerminal();

    HelpEndpoint.Render(terminal);

    string output = terminal.Output;
    await Assert.That(output).Contains("card");
    await Assert.That(output).Contains("Display HamidReza 'Ashkan' Ashkiyan's business card");
    await Assert.That(output).Contains("ashkan");
    await Assert.That(output).Contains("Alias for card");
    await Assert.That(output).Contains("exit, quit, q");
    await Assert.That(output).Contains("clear-history");
    await Assert.That(output).Contains("--help");
  }

  [Test]
  [NotInParallel]
  public async Task Main_RoutesHelpCommand_ToCommandList()
  {
    int exitCode = await Program.Main(["help"]);

    string text = TestContext.Current!.GetStandardOutput();
    await Assert.That(exitCode).IsEqualTo(0);
    await Assert.That(text).Contains("Commands:");
    await Assert.That(text).Contains("Display HamidReza 'Ashkan' Ashkiyan's business card");
    await Assert.That(text).DoesNotContain("Unknown command");
  }
}
