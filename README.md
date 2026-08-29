[![](https://img.shields.io/nuget/v/soenneker.attio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.attio.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.attio.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.attio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclientutil/)

# Soenneker.Attio.OpenApiClientUtil

Creates and caches an authenticated `AttioOpenApiClient` using Microsoft dependency injection and configuration.

Use this package when an application wants the generated Attio client without manually constructing its `HttpClient`, authentication provider, and Kiota request adapter.

## Installation

```bash
dotnet add package Soenneker.Attio.OpenApiClientUtil
```

## Configuration

```json
{
  "Attio": {
    "ApiKey": "your-attio-access-token"
  }
}
```

Requests use `https://api.attio.com` and `Authorization: Bearer {token}` by default. `Attio:ClientBaseUrl`, `Attio:AuthHeaderName`, and `Attio:AuthHeaderValueTemplate` can override those values for a compatible proxy or alternate authentication scheme.

## Registration

```csharp
using Soenneker.Attio.OpenApiClientUtil.Registrars;

builder.Services.AddAttioOpenApiClientUtilAsSingleton();
```

The registrar also adds the required Attio HTTP client services. Use `AddAttioOpenApiClientUtilAsScoped()` if each DI scope should own a utility instance; the underlying HTTP client cache is still process-wide.

## Usage

```csharp
using Soenneker.Attio.OpenApiClientUtil.Abstract;

public sealed class WorkspaceService(IAttioOpenApiClientUtil clientUtil)
{
    public async Task<string?> GetWorkspaceName(CancellationToken cancellationToken)
    {
        var client = await clientUtil.Get(cancellationToken);
        var tokenInfo = await client.V2.Self.GetAsync(
            cancellationToken: cancellationToken);

        return tokenInfo?.WorkspaceName;
    }
}
```

## Lifecycle and configuration behavior

- The generated client is created on the first `Get()` call and reused afterward.
- Concurrent first calls share the same asynchronous initialization.
- Authentication configuration is read during initialization; later changes do not alter the cached client.
- A missing API key fails initialization rather than creating an unauthenticated client.
- Let the DI container dispose the utility. Disposal releases the cached generated client state.

If you need complete control over the Kiota request adapter, authentication provider, or `HttpClient`, reference `Soenneker.Attio.OpenApiClient` directly instead.
