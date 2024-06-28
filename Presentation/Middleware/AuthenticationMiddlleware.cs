using Domain.Publishing.Repositories;
using Domain.Publishing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace WS51_MyEvent.Middleware;

public class AuthenticationMiddlleware
{
    private readonly RequestDelegate _next;
 

    public AuthenticationMiddlleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context,ITokenService tokenService,ICustomerRepository customerRepository)
    {
        //attrubute allow anonymus
        var allowAnonymous = await IsAllowAnonymousAsync(context);

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }
        
        //If token exists
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is missing");
            return;
        }
        //validate token
        var customerId = await tokenService.ValidateToken(token);

        if (customerId == null || customerId == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is corrupted");
            return;
        }

        var customer = await customerRepository.GetByIdAsync(customerId.Value);
        context.Items["Customer"] = customer;
        
        await _next(context);
    }
    
    private Task<bool> IsAllowAnonymousAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null) return Task.FromResult(false);

        var allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null;

        if (!allowAnonymous)
        {
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor != null)
                allowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
        }

        return Task.FromResult(allowAnonymous);
    }
}