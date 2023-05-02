using Ardalis.Specification;
using EasyCaching.Core;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Infrastructure.Cache;
using KavirTire.Shop.Infrastructure.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
#pragma warning disable CS8603

namespace KavirTire.Shop.Infrastructure.Persistence.Repository;

public class CachedRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{
  private readonly ILogger<CachedRepository<T>> _logger;
  private readonly EfRepository<T> _sourceRepository;
  private readonly IEasyCachingProvider _cacheProvider;

  public CachedRepository(
      ILogger<CachedRepository<T>> logger,
      EfRepository<T> sourceRepository,
      IEasyCachingProvider cacheProvider)
  {
    _logger = logger;
    _sourceRepository = sourceRepository;
    _cacheProvider = cacheProvider;
  }
  public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-AnyAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.AnyAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.AnyAsync(specification, cancellationToken);
  }

  public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
     
    string key = $"{typeof(T).Name}-AnyAsync";
    return _cacheProvider.GetOrCreateAsync(key, () => _sourceRepository.AnyAsync(cancellationToken));
  }

  public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-CountAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.CountAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.CountAsync(specification, cancellationToken);
  }

  public Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    string key = $"{typeof(T).Name}-CountAsync";
    return _cacheProvider.GetOrCreateAsync(key, () => _sourceRepository.CountAsync(cancellationToken));
  }

  public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
  {
    string key = $"{typeof(T).Name}-GetByIdAsync-{id}";
    return _cacheProvider.GetOrCreateAsync(key, () => _sourceRepository.GetByIdAsync(id, cancellationToken));
  }

  public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
  {
    string key = $"{typeof(T).Name}-GetByIdAsync-{id}";
    return _cacheProvider.GetOrCreateAsync(key, () => _sourceRepository.GetByIdAsync(id, cancellationToken));
  }

  [Obsolete("Obsolete")]
  public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
  }
  
  [Obsolete("Obsolete")]
  public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification,
    CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.GetBySpecAsync<TResult>(specification, cancellationToken);
  }

  public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-FirstOrDefaultAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-FirstOrDefaultAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  public virtual async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-SingleOrDefaultAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-SingleOrDefaultAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    string key = $"{typeof(T).Name}-ListAsync";
    return _cacheProvider.GetOrCreateAsync(key, () => _sourceRepository.ListAsync(cancellationToken));
  }

  public async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.ListAsync(specification, cancellationToken);
  }

  public async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      _logger.LogInformation("Checking cache for " + key);
      return await _cacheProvider.GetOrCreateAsync(key, () =>
      {
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return await _sourceRepository.ListAsync(specification, cancellationToken);
  }
}