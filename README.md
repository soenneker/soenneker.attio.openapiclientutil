[![](https://img.shields.io/nuget/v/soenneker.attio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.attio.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.attio.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.attio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclientutil/)

# Soenneker.Attio.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Attio.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Attio.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAttioOpenApiClientUtilAsSingleton();
```

Adds `AttioOpenApiClientUtil` as a singleton service.

## What you get

- `IAttioOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `AttioOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AttioOpenApiClientUtilRegistrar.AddAttioOpenApiClientUtilAsSingleton(services)` | Adds `AttioOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AttioOpenApiClientUtilRegistrar.AddAttioOpenApiClientUtilAsScoped(services)` | Adds `AttioOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
