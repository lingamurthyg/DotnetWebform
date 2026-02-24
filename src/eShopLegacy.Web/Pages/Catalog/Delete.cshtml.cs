using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.Catalog;

public class DeleteModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ICatalogItemService catalogItemService, ILogger<DeleteModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public int Id { get; set; }

    public CatalogItemDto? Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading delete confirmation for catalog item {Id}", id);
            Item = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (Item == null)
            {
                _logger.LogWarning("Catalog item {Id} not found", id);
                return NotFound();
            }

            Id = id;
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item {Id} for deletion", id);
            TempData["ErrorMessage"] = "An error occurred while loading the item. Please try again.";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item {Id}", Id);
            await _catalogItemService.DeleteAsync(Id, cancellationToken);
            TempData["SuccessMessage"] = "Catalog item deleted successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item {Id}", Id);
            TempData["ErrorMessage"] = "An error occurred while deleting the item. Please try again.";
            return RedirectToPage("Index");
        }
    }
}
