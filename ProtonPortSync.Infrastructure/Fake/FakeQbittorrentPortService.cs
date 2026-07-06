using ProtonPortSync.Application.Abstractions;

namespace ProtonPortSync.Infrastructure.Fake
{
    public class FakeQbittorrentPortService : IQbittorrentPortService
    {
        private int _currentPort = 54321;
        public Task<int> GetCurrentPortAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_currentPort);
        }

        public Task UpdatePortAsync(int newPort, CancellationToken cancellationToken)
        {
            _currentPort = newPort;
            return Task.CompletedTask;
        }
    }
}
