using Soenneker.Attio.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Attio.OpenApiClientUtil.Abstract;
/// <summary>
/// Creates and caches an authenticated <see cref="AttioOpenApiClient"/>.
/// </summary>
public interface IAttioOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached generated client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The cached Attio client.</returns>
    ValueTask<AttioOpenApiClient> Get(CancellationToken cancellationToken = default);
}
