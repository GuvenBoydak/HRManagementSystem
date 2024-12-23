using System.Net;
using HrManagement.Application;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController:ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() },
            HttpStatusCode.Created => new CreatedResult(result.UrlAsCreated, result),
            _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
        };
    }
    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null){StatusCode = result.StatusCode.GetHashCode()};
        }
        return new ObjectResult(result){StatusCode = result.StatusCode.GetHashCode()};
    }
}