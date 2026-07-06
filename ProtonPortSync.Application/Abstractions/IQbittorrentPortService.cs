using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPortSync.Application.Abstractions
{
    public interface IQbittorrentPortService
    {
        Task<int> GetCurrentPortAsync(CancellationToken cancellationToken);
        Task UpdatePortAsync(int port, CancellationToken cancellationToken);
    }
}
