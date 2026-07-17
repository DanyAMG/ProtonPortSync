using ProtonPortSync.Application.Abstractions;
using ProtonPortSync.Infrastructure.Configuration;

namespace ProtonPortSync.Infrastructure.Qbittorrent
{
    public class QbittorrentPortService : IQbittorrentPortService
    {
        public QbittorrentPortService(HttpClient httpClient, QbittorrentOptions options)
        {
            // Constructor implementation
        }

        public Task<int> GetCurrentPortAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        public Task UpdatePortAsync(int port, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
