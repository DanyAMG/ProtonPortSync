using System;
using System.Collections.Generic;
using System.Text;
using ProtonPortSync.Application.Abstractions;
using ProtonPortSync.Domain.Models;

namespace ProtonPortSync.Application.UseCases
{
    public sealed class SynchronizePortsUseCase
    {
        private readonly IProtonPortProvider _protonPortProvider;
        private readonly IQbittorrentPortService _qbittorrentPortService;

        public SynchronizePortsUseCase(IProtonPortProvider protonPortProvider, IQbittorrentPortService qbittorrentPortService)
        {
            _protonPortProvider = protonPortProvider;
            _qbittorrentPortService = qbittorrentPortService;
        }

        public async Task<SynchronizationResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var protonPort = await _protonPortProvider.GetCurrentPortAsync(cancellationToken);

            if (protonPort == null)
            {
                throw new InvalidOperationException("No Proton port was detected.");
            }

            var qbittorrentPortBefore = await _qbittorrentPortService.GetCurrentPortAsync(cancellationToken);

            if (protonPort.Port == qbittorrentPortBefore)
            {
                return new SynchronizationResult(
                    WasUpdated: false,
                    IsSynchronized: qbittorrentPortBefore == protonPort.Port,
                    ProtonPort: protonPort.Port,
                    QbittorrentPortBefore: qbittorrentPortBefore,
                    QbittorrentPortAfter: qbittorrentPortBefore,
                    SynchronizedAt: DateTimeOffset.UtcNow);
            }
                await _qbittorrentPortService.UpdatePortAsync(
                protonPort.Port,
                cancellationToken);

                var qbittorrentPortAfter = await _qbittorrentPortService.GetCurrentPortAsync(cancellationToken);

                return new SynchronizationResult(
                    WasUpdated: true,
                    IsSynchronized: qbittorrentPortAfter == protonPort.Port,
                    ProtonPort: protonPort.Port,
                    QbittorrentPortBefore: qbittorrentPortBefore,
                    QbittorrentPortAfter: qbittorrentPortAfter,
                    SynchronizedAt: DateTimeOffset.UtcNow);
        }
    }
}
