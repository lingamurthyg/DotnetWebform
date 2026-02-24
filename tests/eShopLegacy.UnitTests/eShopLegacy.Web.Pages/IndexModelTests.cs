using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using eShopLegacy.Web.Pages;

namespace eShopLegacy.Web.Pages.Tests;

public class IndexModelTests
{
    private readonly Mock<ILogger<IndexModel>> _loggerMock;

    public IndexModelTests()
    {
        _loggerMock = new Mock<ILogger<IndexModel>>();
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        var model = new IndexModel(_loggerMock.Object);

        Assert.NotNull(model);
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new IndexModel(null));
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutException()
    {
        var model = new IndexModel(_loggerMock.Object);

        model.OnGet();

        Assert.True(true);
    }
}
