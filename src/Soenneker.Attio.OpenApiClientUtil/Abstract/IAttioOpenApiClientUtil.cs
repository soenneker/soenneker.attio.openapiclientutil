using Soenneker.Attio.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Attio.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IAttioOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<AttioOpenApiClient> Get(CancellationToken cancellationToken = default);
}
