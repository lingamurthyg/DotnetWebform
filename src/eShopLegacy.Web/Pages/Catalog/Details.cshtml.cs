using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.Catalog;

public class DetailsModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICatalogItemService catalogItemService, ILogger<DetailsModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public CatalogItemDto? Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading details for catalog item {Id}", id);
            Item = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (Item == null)
            {
                _logger.LogWarning("Catalog item {Id} not found", id);
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item {Id}", id);
            TempData["ErrorMessage"] = "An error occurred while loading the item. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
