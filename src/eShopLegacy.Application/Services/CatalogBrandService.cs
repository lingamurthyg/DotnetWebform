using AutoMapper;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository repository,
        IMapper mapper,
        ILogger<CatalogBrandService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogBrandDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog brands");
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogBrandDto>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog brands");
            throw;
        }
    }

    public async Task<CatalogBrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog brand with id {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity == null ? null : _mapper.Map<CatalogBrandDto>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrandDto> CreateAsync(CatalogBrandCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog brand: {Brand}", dto.Brand);
            var entity = _mapper.Map<CatalogBrand>(dto);
            var created = await _repository.AddAsync(entity, cancellationToken);
            _logger.LogInformation("Created catalog brand with id {Id}", created.Id);
            return _mapper.Map<CatalogBrandDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog brand: {Brand}", dto.Brand);
            throw;
        }
    }

    public async Task<CatalogBrandDto> UpdateAsync(int id, CatalogBrandUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog brand with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                _logger.LogWarning("Catalog brand with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog brand with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
            _logger.LogInformation("Updated catalog brand with id {Id}", id);
            return _mapper.Map<CatalogBrandDto>(existing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog brand with id {Id}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Catalog brand with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog brand with id {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog brand with id {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with id {Id}", id);
            throw;
        }
    }
}
