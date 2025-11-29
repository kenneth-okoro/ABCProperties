namespace ABCProperties.Application
{
    public class CacheSettings
    {
        public int SlidingExpiration { get; set; }
        public int AbsoluteExpiration { get; set; }
        public string DestinationUrl { get; set; }
        public string ApplicationName { get; set; }
        public bool IsByPassCache { get; set; }
    }
}
