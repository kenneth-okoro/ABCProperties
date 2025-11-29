using ABCProperties.Application.Pipelines.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace ABCProperties.Application.Pipelines
{
    public class CachePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachePipelineBehaviour(IDistributedCache cache, IOptions<CacheSettings> cacheSettingsOptions)
        {
            _cache = cache;
            _cacheSettings = cacheSettingsOptions.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (request.IsBypassCache) return await next();

            TResponse response;

            string cacheKey = $"{_cacheSettings.ApplicationName}_{request.CacheKey}";
            var cachedResponse = await _cache.GetAsync(cacheKey, cancellationToken);

            if (cachedResponse is not null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            }
            else
            {
                response = await GetResponseAndWriteToCacheAsync();
            }

            return response;

            // Reading from the handler class -> Db
            async Task<TResponse> GetResponseAndWriteToCacheAsync()
            {
                response = await next();

                if (response is not null)
                {
                    var slidingExpiration = request.SlidingExpiration == null ?
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration) 
                        : request.SlidingExpiration.Value;

                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now
                            .AddMinutes(_cacheSettings.AbsoluteExpiration)
                    };

                    var serializedData = Encoding.Default
                        .GetBytes(JsonConvert.SerializeObject(response, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));

                    await _cache.SetAsync(cacheKey, serializedData, cacheOptions, cancellationToken);
                }

                return response;
            }
        }
    }
}
