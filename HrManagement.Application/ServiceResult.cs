using System.Net;
using System.Text.Json.Serialization;

namespace HrManagement.Application;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages?.Count == 0;

    [JsonIgnore] public bool IsFailure => !IsSuccess;
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = statusCode
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }

    public static ServiceResult<T> Failure(string messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessages = [messages],
            StatusCode = statusCode
        };
    }

    public static ServiceResult<T> Failure(List<string> messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessages = messages,
            StatusCode = statusCode
        };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMessages { get; set; }

    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages?.Count > 0;
    [JsonIgnore] public bool IsFailure => !IsSuccess;

    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            StatusCode = statusCode
        };
    }

    public static ServiceResult Failure(string messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessages = [messages],
            StatusCode = statusCode
        };
    }

    public static ServiceResult Failure(List<string> messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessages = messages,
            StatusCode = statusCode
        };
    }
}