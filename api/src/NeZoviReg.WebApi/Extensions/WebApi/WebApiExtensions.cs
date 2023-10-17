using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.WebApi.Extensions.WebApi;

public static class WebApiExtensions
{
    public static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Dictionary<string, string[]>? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

    /*public static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };*/
    
    public static string GetFirstOrDefault(this IFormCollection form, string key) => form?[key].FirstOrDefault();

    public static string GetFirstOrDefault(this IQueryCollection query, string key) => query?[key].FirstOrDefault();
}