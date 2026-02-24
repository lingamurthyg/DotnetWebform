using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.Catalog;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogItemService catalogItemService, ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<CatalogItemDto> Items { get; set; } = new List<CatalogItemDto>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
    public long TotalCount { get; set; }
    public int TotalPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading catalog page {PageIndex} with page size {PageSize}", pageIndex, pageSize);

            var paginatedResult = await _catalogItemService.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);

            Items = paginatedResult.Data;
            PageIndex = paginatedResult.PageIndex;
            PageSize = paginatedResult.PageSize;
            TotalCount = paginatedResult.TotalCount;
            TotalPages = paginatedResult.TotalPages;

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            TempData["ErrorMessage"] = "An error occurred while loading the catalog. Please try again.";
            return Page();
        }
    }
}
