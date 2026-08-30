using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Attio.HttpClients.Abstract;
using Soenneker.Attio.OpenApiClientUtil.Abstract;
using Soenneker.Attio.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Attio.OpenApiClientUtil;

public sealed class AttioOpenApiClientUtil : IAttioOpenApiClientUtil
{
    private readonly AsyncSingleton<AttioOpenApiClient> _client;

    public AttioOpenApiClientUtil(IAttioOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<AttioOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Attio:ApiKey");
            string authHeaderName = configuration["Attio:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Attio:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new AttioOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<AttioOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
