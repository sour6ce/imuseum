using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IMuseum.Persistence.Models;
using Microsoft.AspNetCore.Http;

namespace IMuseum.Auth.Authorization;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CatalogManagerAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = (User)context.HttpContext.Items["User"];
        if(user!=null){
            foreach (var r in user.Roles){
                if(r.Name == "Catalog Manager"){
                    return;
                }
            }
            // not logged in - return 401 unauthorized
            context.Result = new JsonResult(new { message = "Insufficient permission level to perform this action" }) { StatusCode = StatusCodes.Status401Unauthorized };

            // set 'WWW-Authenticate' header to trigger login popup in browsers
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        }
        else
        {
            // not logged in - return 401 unauthorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            // set 'WWW-Authenticate' header to trigger login popup in browsers
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        }
    }
}