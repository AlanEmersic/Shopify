using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Shopify.Application.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shopify.Infrastructure.Authentication.Filters;

internal sealed class AuthenticationRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo.DeclaringType!
            .GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes.Any())
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme } }
                    ] = Array.Empty<string>()
                }
            };
        }
    }
}