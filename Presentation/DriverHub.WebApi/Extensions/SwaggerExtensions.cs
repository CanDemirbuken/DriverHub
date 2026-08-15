using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi;

namespace DriverHub.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(
       this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "DriverHub API",
                Version = "v2",
                Description =
                    "DriverHub araç kiralama uygulaması API servisleri."
            });

            options.TagActionsBy(api =>
            {
                if (api.ActionDescriptor is not ControllerActionDescriptor descriptor)
                    return ["Other"];

                string controllerName = descriptor.ControllerName;
                string? controllerNamespace =
                    descriptor.ControllerTypeInfo.Namespace;

                if (controllerNamespace?.Contains(
                        ".Controllers.Identity") == true)
                {
                    return [$"Identity / {controllerName}"];
                }

                if (controllerNamespace?.Contains(
                        ".Controllers.Entities") == true)
                {
                    return [$"Entities / {controllerName}"];
                }

                return [controllerName];
            });

            options.OrderActionsBy(api =>
            {
                if (api.ActionDescriptor is not ControllerActionDescriptor descriptor)
                    return api.RelativePath ?? string.Empty;

                string? controllerNamespace =
                    descriptor.ControllerTypeInfo.Namespace;

                string groupOrder =
                    controllerNamespace?.Contains(
                        ".Controllers.Entities") == true
                        ? "1"
                        : controllerNamespace?.Contains(
                            ".Controllers.Identity") == true
                            ? "2"
                            : "9";

                return
                    $"{groupOrder}_{descriptor.ControllerName}_{api.HttpMethod}";
            });

            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Access token değerini yalnızca token olarak girin. " +
                        "'Bearer' ifadesini eklemeyin."
                });

            options.AddSecurityRequirement(
                document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                        "Bearer",
                        document)] = []
                });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                "/swagger/v2/swagger.json",
                "DriverHub API v2");

            options.RoutePrefix = "swagger";
        });

        return app;
    }
}