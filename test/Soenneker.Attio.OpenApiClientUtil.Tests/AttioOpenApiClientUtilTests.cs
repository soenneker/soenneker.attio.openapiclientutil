using Soenneker.Attio.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Attio.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AttioOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IAttioOpenApiClientUtil _openapiclientutil;

    public AttioOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IAttioOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
