using ProtonPortSync.Application.Abstractions;
using ProtonPortSync.Domain.Models;

namespace ProtonPortSync.Infrastructure.Fake
{
    public class FakeProtonPortProvider : IProtonPortProvider
    {
        public Task<PortForwardInfo?> GetCurrentPortAsync(CancellationToken cancellationToken)
        {
            var protonPort = new PortForwardInfo(12345, DateTimeOffset.UtcNow);
            return Task.FromResult<PortForwardInfo?>(protonPort);
        }
    }
}
