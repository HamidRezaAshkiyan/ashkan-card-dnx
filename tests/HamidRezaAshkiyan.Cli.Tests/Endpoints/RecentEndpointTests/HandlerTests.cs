using System.Net.Http;
using System.Threading;
using HamidRezaAshkiyan.Cli.Endpoints;

namespace HamidRezaAshkiyan.Cli.Tests.Endpoints.RecentEndpointTests;

/// <summary>
/// Tests for RecentEndpoint.Handler.GetRecentActivitiesWithVerboseAsync() — CRAP 210,
/// cyclomatic complexity 14. The verbose path iterates each source sequentially and
/// has distinct catch branches for HttpRequestException (404/403/500/other),
/// TaskCanceledException, and generic Exception. Also exercises the non-verbose path
/// and the DisplayTable/GetRelativeTimeString code via a fake Atom feed.
/// </summary>
public class HandlerTests
{
    // Minimal Atom XML understood by ParseYouTubeAtom and ParseGitHubAtom (both use
    // http://www.w3.org/2005/Atom and look for <entry> descendants).
    private const string AtomXml = """
        <?xml version="1.0" encoding="utf-8"?>
        <feed xmlns="http://www.w3.org/2005/Atom">
          <entry>
            <title>Test Entry</title>
            <link href="https://github.com/hamidrezaashkiyan/ashkan-card-dnx" />
            <published>2026-01-16T12:00:00Z</published>
            <updated>2026-01-16T12:00:00Z</updated>
          </entry>
        </feed>
        """;

    // --- Verbose=true: HttpRequestException branches ---

    // Note: RecentEndpoint.Handler calls CreateClient() *outside* its per-source try-catch,
    // so ThrowingHttpClientFactory (which throws in CreateClient) would propagate unhandled.
    // ThrowingOnRequestHttpClientFactory creates a real client whose SendAsync throws,
    // which is then caught by the per-source exception handlers.

    [Test]
    public async Task Handle_DoesNotThrow_WhenHttpRequestFails_Verbose()
    {
        var handler = new RecentEndpoint.Handler(new ThrowingOnRequestHttpClientFactory());
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenHttp404Error_Verbose()
    {
        var factory = new ThrowingOnRequestHttpClientFactory(new HttpRequestException("404 Not Found"));
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenHttp403Error_Verbose()
    {
        var factory = new ThrowingOnRequestHttpClientFactory(new HttpRequestException("403 Forbidden"));
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenHttp500Error_Verbose()
    {
        var factory = new ThrowingOnRequestHttpClientFactory(new HttpRequestException("500 Internal Server Error"));
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenTaskCanceled_Verbose()
    {
        var factory = new ThrowingOnRequestHttpClientFactory(new TaskCanceledException("Request timed out"));
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenGenericException_Verbose()
    {
        var factory = new ThrowingOnRequestHttpClientFactory(new InvalidOperationException("unexpected error"));
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    // --- Verbose=false ---

    [Test]
    public async Task Handle_DoesNotThrow_WhenHttpRequestFails_NotVerbose()
    {
        var handler = new RecentEndpoint.Handler(new ThrowingOnRequestHttpClientFactory());
        var query = new RecentEndpoint { Verbose = false };

        await handler.Handle(query, CancellationToken.None);
    }

    // --- Success path: exercises DisplayTable → GetRelativeTimeString ---

    [Test]
    public async Task Handle_DoesNotThrow_WhenFeedReturnsValidAtom_NotVerbose()
    {
        // YouTube + GitHub parsers both find <entry> in the Atom feed → non-empty activities list →
        // DisplayTable() is called → GetRelativeTimeString() is exercised.
        var factory = new FakeHttpClientFactory(AtomXml);
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = false };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenFeedReturnsValidAtom_Verbose()
    {
        var factory = new FakeHttpClientFactory(AtomXml);
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }

    [Test]
    public async Task Handle_DoesNotThrow_WhenFeedReturnsValidBlogRss_Verbose()
    {
        const string rssXml = """
            <?xml version="1.0" encoding="utf-8"?>
            <rss version="2.0">
              <channel>
                <title>Hamid Reza Ashkiyan — Engineering Blog</title>
                <item>
                  <title>Migrating 15+ Production Services to Modern .NET</title>
                  <link>https://hamidrezaashkiyan.github.io/blog/migrating-15-microservices-to-dotnet-8.html</link>
                  <pubDate>Sun, 22 Nov 2026 00:00:00 +0000</pubDate>
                </item>
              </channel>
            </rss>
            """;

        var factory = new FakeHttpClientFactory(rssXml);
        var handler = new RecentEndpoint.Handler(factory);
        var query = new RecentEndpoint { Verbose = true };

        await handler.Handle(query, CancellationToken.None);
    }
}
