using HamidRezaAshkiyan.Cli.Endpoints;
using TimeWarp.Terminal;

namespace HamidRezaAshkiyan.Cli.Tests.Endpoints.ProjectsEndpointTests;

/// <summary>
/// Tests for ProjectsEndpoint, which displays HamidReza's curated engineering projects.
/// </summary>
public class HandlerTests
{
  [Test]
  public async Task GetProjects_ReturnsAllCategories_WhenNoFilterProvided()
  {
    var projects = ProjectsEndpoint.GetProjects();
    var names = projects.Select(p => p.Name).ToList();

    await Assert.That(names).Contains("DekaSeir");
    await Assert.That(names).Contains("ModularMonolith");
    await Assert.That(names).Contains("aegis");
    await Assert.That(names).Contains("Hafez");
    await Assert.That(names).Contains("agent-bridge");
  }

  [Test]
  public async Task GetProjects_FiltersByCategory_CaseInsensitively()
  {
    var systems = ProjectsEndpoint.GetProjects("SYSTEMS");

    await Assert.That(systems.Count).IsGreaterThan(0);
    await Assert.That(systems.All(p => p.Category == "systems")).IsTrue();
    await Assert.That(systems.Select(p => p.Name)).Contains("aegis");
  }

  [Test]
  public async Task Render_WritesProjectsTableAndPortfolioLink()
  {
    using var terminal = new TestTerminal();

    ProjectsEndpoint.Render(terminal);

    string output = terminal.Output;
    await Assert.That(output).Contains("HamidReza's Featured Engineering Projects");
    await Assert.That(output).Contains("DekaSeir");
    await Assert.That(output).Contains("ModularMonolith");
    await Assert.That(output).Contains("hamidrezaashkiyan.github.io");
  }

  [Test]
  public async Task Render_WritesHelpfulMessage_WhenCategoryUnknown()
  {
    using var terminal = new TestTerminal();

    ProjectsEndpoint.Render(terminal, "nonexistent");

    string output = terminal.Output;
    await Assert.That(output).Contains("No projects found for category 'nonexistent'.");
    await Assert.That(output).Contains("backend, systems, desktop, ai");
  }
}
