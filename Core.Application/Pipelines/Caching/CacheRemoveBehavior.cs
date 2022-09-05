using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching
{
    public class CacheRemoveBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheRemoveBehavior<TRequest, TResponse>> _logger;

        public CacheRemoveBehavior(IDistributedCache cache, ILogger<CacheRemoveBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (request.BypassCache) return await next();

            async Task<TResponse> GetResponseAndRemoveCache()
            {
                response = await next();
                await _cache.RemoveAsync(request.CacheKey, cancellationToken);
                return response;
            }

            response = await GetResponseAndRemoveCache();
            _logger.LogInformation($"Removed Cache -> {request.CacheKey}");

            return response;
        }
    }
}
