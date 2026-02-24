using System.ComponentModel.DataAnnotations;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopLegacy.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService catalogBrandService,
        ICatalogTypeService catalogTypeService,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _catalogBrandService = catalogBrandService ?? throw new ArgumentNullException(nameof(catalogBrandService));
        _catalogTypeService = catalogTypeService ?? throw new ArgumentNullException(nameof(catalogTypeService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public CatalogItemInputModel Input { get; set; } = new();

    public SelectList BrandOptions { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList TypeOptions { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _catalogItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }

            Id = item.Id;
            Input = new CatalogItemInputModel
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureFileName = item.PictureFileName,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                AvailableStock = item.AvailableStock,
                RestockThreshold = item.RestockThreshold,
                MaxStockThreshold = item.MaxStockThreshold,
                OnReorder = item.OnReorder
            };

            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item {Id}", id);
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }

        try
        {
            var dto = new CatalogItemUpdateDto
            {
                Name = Input.Name,
                Description = Input.Description,
                Price = Input.Price,
                PictureFileName = Input.PictureFileName,
                CatalogBrandId = Input.CatalogBrandId,
                CatalogTypeId = Input.CatalogTypeId,
                AvailableStock = Input.AvailableStock,
                RestockThreshold = Input.RestockThreshold,
                MaxStockThreshold = Input.MaxStockThreshold,
                OnReorder = Input.OnReorder
            };

            await _catalogItemService.UpdateAsync(Id, dto, cancellationToken);
            TempData["SuccessMessage"] = "Catalog item updated successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item {Id}", Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the item. Please try again.");
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadSelectListsAsync(CancellationToken cancellationToken)
    {
        var brands = await _catalogBrandService.GetAllAsync(cancellationToken);
        BrandOptions = new SelectList(brands, nameof(CatalogBrandDto.Id), nameof(CatalogBrandDto.Brand));

        var types = await _catalogTypeService.GetAllAsync(cancellationToken);
        TypeOptions = new SelectList(types, nameof(CatalogTypeDto.Id), nameof(CatalogTypeDto.Type));
    }

    public class CatalogItemInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? PictureFileName { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int CatalogBrandId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int CatalogTypeId { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Available Stock")]
        public int AvailableStock { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Restock Threshold")]
        public int RestockThreshold { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Max Stock Threshold")]
        public int MaxStockThreshold { get; set; }

        [Display(Name = "On Reorder")]
        public bool OnReorder { get; set; }
    }
}
