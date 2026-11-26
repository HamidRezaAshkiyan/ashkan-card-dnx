using System.Threading;
using System.Threading.Tasks;
using TimeWarp.Nuru;
using static HamidRezaAshkiyan.Cli.Urls;
using static HamidRezaAshkiyan.Cli.Helpers.UrlHelper;

namespace HamidRezaAshkiyan.Cli.Endpoints;

/// <summary>
/// URL opener endpoints for opening HamidReza 'Ashkan' Ashkiyan's profiles in the browser.
/// </summary>
[NuruRoute("blog", Description = "Open HamidReza's engineering blog")]
public sealed class BlogEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<BlogEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(BlogEndpoint cmd, CancellationToken ct)
    {
      Open(Blog);
      return default;
    }
  }
}

[NuruRoute("contact", Description = "Open HamidReza's contact section")]
public sealed class ContactEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<ContactEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(ContactEndpoint cmd, CancellationToken ct)
    {
      Open(Contact);
      return default;
    }
  }
}

[NuruRoute("github", Description = "Open HamidReza's GitHub profile")]
public sealed class GithubEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<GithubEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(GithubEndpoint cmd, CancellationToken ct)
    {
      Open(GitHub);
      return default;
    }
  }
}

[NuruRoute("linkedin", Description = "Open HamidReza's LinkedIn profile")]
public sealed class LinkedinEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<LinkedinEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(LinkedinEndpoint cmd, CancellationToken ct)
    {
      Open(LinkedIn);
      return default;
    }
  }
}

[NuruRoute("nuget", Description = "Open HamidReza's NuGet profile")]
public sealed class NugetEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<NugetEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(NugetEndpoint cmd, CancellationToken ct)
    {
      Open(NuGet);
      return default;
    }
  }
}

[NuruRoute("resume", Description = "Open HamidReza's printable resume")]
[NuruRouteAlias("cv")]
public sealed class ResumeEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<ResumeEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(ResumeEndpoint cmd, CancellationToken ct)
    {
      Open(Resume);
      return default;
    }
  }
}

[NuruRoute("telegram", Description = "Open HamidReza's Telegram profile")]
public sealed class TelegramEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<TelegramEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(TelegramEndpoint cmd, CancellationToken ct)
    {
      Open(Telegram);
      return default;
    }
  }
}

[NuruRoute("website", Description = "Open HamidReza's personal website")]
[NuruRouteAlias("web", "site", "portfolio")]
public sealed class WebsiteEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<WebsiteEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(WebsiteEndpoint cmd, CancellationToken ct)
    {
      Open(Website);
      return default;
    }
  }
}

[NuruRoute("youtube", Description = "Open HamidReza's YouTube channel (thisistechsin)")]
public sealed class YoutubeEndpoint : ICommand<Unit>
{
  public sealed class Handler : ICommandHandler<YoutubeEndpoint, Unit>
  {
    public ValueTask<Unit> Handle(YoutubeEndpoint cmd, CancellationToken ct)
    {
      Open(YouTube);
      return default;
    }
  }
}
