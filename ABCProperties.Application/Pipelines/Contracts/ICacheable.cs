namespace ABCProperties.Application.Pipelines.Contracts
{
    public interface ICacheable
    {
        public string CacheKey { get; set; }
        public bool IsBypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
