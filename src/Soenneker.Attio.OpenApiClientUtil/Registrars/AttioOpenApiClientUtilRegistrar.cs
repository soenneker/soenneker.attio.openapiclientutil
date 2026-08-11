using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Attio.HttpClients.Registrars;
using Soenneker.Attio.OpenApiClientUtil.Abstract;

namespace Soenneker.Attio.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class AttioOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="AttioOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAttioOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddAttioOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IAttioOpenApiClientUtil, AttioOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="AttioOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAttioOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddAttioOpenApiHttpClientAsSingleton()
                .TryAddScoped<IAttioOpenApiClientUtil, AttioOpenApiClientUtil>();

        return services;
    }
}
