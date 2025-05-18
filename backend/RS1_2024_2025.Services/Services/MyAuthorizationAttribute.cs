namespace RS1_2024_2025.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RS1_2024_2025.Domain.Entities;
using System;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MyAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly UserType[] _allowedRoles;

    public MyAuthorizationAttribute(params UserType[] allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authService = context.HttpContext.RequestServices.GetService<MyAuthService>();
        if (authService == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authInfo = authService.GetAuthInfo();
        if (!authInfo.IsLoggedIn || !_allowedRoles.Contains(authInfo.UserType))
        {
            context.Result = new ForbidResult();
        }
    }
}

