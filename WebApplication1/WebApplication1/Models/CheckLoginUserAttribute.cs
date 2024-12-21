using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class CheckLoginUserAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var isLogged = context.HttpContext.Session.GetInt32("isLogin");
        if (isLogged == null || isLogged == 0)
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
            return;
        }

        // Add HTTP response headers
        var response = context.HttpContext.Response;
        if (response != null)
        {
            response.Headers.Add("Set-Cookie", "HttpOnly;Secure;SameSite=Strict");
            // Add more headers...
        }

        base.OnActionExecuting(context);
    }
}
