using Shopify.Application;
using Shopify.Domain;
using Shopify.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Host);

WebApplication app = builder.Build();

app.UseInfrastructure(app.Environment);

app.Run();