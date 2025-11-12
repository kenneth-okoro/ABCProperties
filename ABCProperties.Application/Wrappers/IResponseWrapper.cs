using System.Text.Json.Serialization;

namespace ABCProperties.Application.Wrappers
{
    public interface IResponseWrapper
    {
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
    }

    public interface IResponseWrapper<out T> : IResponseWrapper
    {
        public T Data { get; }
    }
}
