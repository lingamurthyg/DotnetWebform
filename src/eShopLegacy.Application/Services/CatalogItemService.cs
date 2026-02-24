using AutoMapper;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

/// <summary>
/// Service implementation for CatalogItem operations
/// </summary>
public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        ICatalogItemRepository repository,
        IMapper mapper,
        ILogger<CatalogItemService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog items");
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog items");
            throw;
        }
    }

    public async Task<CatalogItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog item with id {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity == null ? null : _mapper.Map<CatalogItemDto>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<PaginatedItemsDto<CatalogItemDto>> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paginated catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);
            var (items, totalCount) = await _repository.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);
            var itemDtos = _mapper.Map<IEnumerable<CatalogItemDto>>(items);

            return new PaginatedItemsDto<CatalogItemDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = itemDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paginated catalog items");
            throw;
        }
    }

    public async Task<CatalogItemDto> CreateAsync(CatalogItemCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item: {Name}", dto.Name);
            var entity = _mapper.Map<CatalogItem>(dto);
            var created = await _repository.AddAsync(entity, cancellationToken);
            _logger.LogInformation("Created catalog item with id {Id}", created.Id);
            return _mapper.Map<CatalogItemDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item: {Name}", dto.Name);
            throw;
        }
    }

    public async Task<CatalogItemDto> UpdateAsync(int id, CatalogItemUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                _logger.LogWarning("Catalog item with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog item with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
            _logger.LogInformation("Updated catalog item with id {Id}", id);
            return _mapper.Map<CatalogItemDto>(existing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item with id {Id}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Catalog item with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog item with id {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog item with id {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItemDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
