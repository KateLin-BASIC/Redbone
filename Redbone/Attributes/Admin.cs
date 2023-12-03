using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Redbone.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Admin : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Session.GetString("_isAuthed") != "true")
            context.Result = new UnauthorizedResult();
    }
}