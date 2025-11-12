
using System.Text.Json.Serialization;

namespace ABCProperties.Application.Wrappers
{
    public class ResponseWrapper : IResponseWrapper
    {
        [JsonPropertyOrder(1)]
        public List<string> Messages { get; set; } = [];
        [JsonPropertyOrder(0)]
        public bool IsSuccessful { get ; set; }

        #region Failures
        public static IResponseWrapper Fail()
        {
            return new ResponseWrapper() { IsSuccessful = false };
        }

        public static IResponseWrapper Fail(string message)
        {
            return new ResponseWrapper() { IsSuccessful = false, Messages = [message] };
        }

        public static IResponseWrapper Fail(List<string> messages)
        {
            return new ResponseWrapper() { IsSuccessful = false, Messages = messages };
        }
        #endregion

        #region Success
        public static IResponseWrapper Success()
        {
            return new ResponseWrapper() { IsSuccessful = true };
        }

        public static IResponseWrapper Success(string message)
        {
            return new ResponseWrapper() { IsSuccessful = true, Messages = [message] };
        }

        public static IResponseWrapper Success(List<string> messages)
        {
            return new ResponseWrapper() { IsSuccessful = true, Messages = messages };
        }
        #endregion
    }

    public class ResponseWrapper<T> : ResponseWrapper, IResponseWrapper<T>
    {
        [JsonPropertyOrder(2)]
        public T Data { get; init; }
        #region Failures
        public new static IResponseWrapper<T> Fail()
        {
            return new ResponseWrapper<T>() { IsSuccessful = false };
        }
        public new static IResponseWrapper<T> Fail(string message)
        {
            return new ResponseWrapper<T>() { IsSuccessful = false, Messages = [message] };
        }
        public new static IResponseWrapper<T> Fail(List<string> messages)
        {
            return new ResponseWrapper<T>() { IsSuccessful = false, Messages = messages };
        }
        #endregion

        #region Success
        // Without Data
        public new static IResponseWrapper<T> Success()
        {
            return new ResponseWrapper<T>() { IsSuccessful = true};
        }
        public new static IResponseWrapper<T> Success(string message)
        {
            return new ResponseWrapper<T>() { IsSuccessful = true, Messages = [message] };
        }
        public new static IResponseWrapper<T> Success(List<string> messages)
        {
            return new ResponseWrapper<T>() { IsSuccessful = true, Messages = messages };
        }

        // With Data
        public static IResponseWrapper<T> Success(T data)
        {
            return new ResponseWrapper<T>() { IsSuccessful = true, Data = data };
        }
        public static IResponseWrapper<T> Success(T data, string message)
        {
            return new ResponseWrapper<T>() { IsSuccessful = true, Messages = [message], Data = data, };
        }
        public static IResponseWrapper<T> Success(T data, List<string> messages)
        {
            return new ResponseWrapper<T>() { IsSuccessful = true, Messages = messages, Data = data, };
        }
        #endregion
    }
}
