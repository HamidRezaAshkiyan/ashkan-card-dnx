using HamidRezaAshkiyan.Cli.Helpers;

namespace HamidRezaAshkiyan.Cli.Tests.Helpers.UrlHelperTests;

/// <summary>
/// Tests for UrlHelper.AddUtmSource — ensures tracking parameters are appended correctly
/// to all URLs opened by the CLI so analytics accurately report CLI traffic.
/// </summary>
public class AddUtmSourceTests
{
    [Test]
    public async Task AddsUtmSourceWithQuestionMark_WhenNoExistingQueryString()
    {
        var result = UrlHelper.AddUtmSource("https://hamidrezaashkiyan.com");

        await Assert.That(result).IsEqualTo("https://hamidrezaashkiyan.com?utm_source=hamidrezaashkiyancli");
    }

    [Test]
    public async Task AppendsUtmSourceWithAmpersand_WhenQueryStringAlreadyPresent()
    {
        var result = UrlHelper.AddUtmSource("https://hamidrezaashkiyan.com?foo=bar");

        await Assert.That(result).IsEqualTo("https://hamidrezaashkiyan.com?foo=bar&utm_source=hamidrezaashkiyancli");
    }

    [Test]
    public async Task ReturnsUnchanged_WhenUtmSourceAlreadyPresent()
    {
        var url = "https://hamidrezaashkiyan.com?utm_source=hamidrezaashkiyancli";

        var result = UrlHelper.AddUtmSource(url);

        await Assert.That(result).IsEqualTo(url);
    }

    [Test]
    public async Task ReturnsUnchanged_WhenUtmSourcePresentWithDifferentCase()
    {
        var url = "https://hamidrezaashkiyan.com?UTM_SOURCE=other";

        var result = UrlHelper.AddUtmSource(url);

        await Assert.That(result).IsEqualTo(url);
    }

    [Test]
    public async Task ReturnsEmptyString_WhenInputIsEmpty()
    {
        var result = UrlHelper.AddUtmSource(string.Empty);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task PreservesMultipleExistingParams_WhenAppendingUtmSource()
    {
        var result = UrlHelper.AddUtmSource("https://hamidrezaashkiyan.com?a=1&b=2");

        await Assert.That(result).IsEqualTo("https://hamidrezaashkiyan.com?a=1&b=2&utm_source=hamidrezaashkiyancli");
    }
}
