using AutoMapper;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository repository,
        IMapper mapper,
        ILogger<CatalogTypeService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog types");
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogTypeDto>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog types");
            throw;
        }
    }

    public async Task<CatalogTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog type with id {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity == null ? null : _mapper.Map<CatalogTypeDto>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogTypeDto> CreateAsync(CatalogTypeCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog type: {Type}", dto.Type);
            var entity = _mapper.Map<CatalogType>(dto);
            var created = await _repository.AddAsync(entity, cancellationToken);
            _logger.LogInformation("Created catalog type with id {Id}", created.Id);
            return _mapper.Map<CatalogTypeDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog type: {Type}", dto.Type);
            throw;
        }
    }

    public async Task<CatalogTypeDto> UpdateAsync(int id, CatalogTypeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog type with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                _logger.LogWarning("Catalog type with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog type with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
            _logger.LogInformation("Updated catalog type with id {Id}", id);
            return _mapper.Map<CatalogTypeDto>(existing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog type with id {Id}", id);
            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Catalog type with id {Id} not found", id);
                throw new KeyNotFoundException($"Catalog type with id {id} not found");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog type with id {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with id {Id}", id);
            throw;
        }
    }
}
