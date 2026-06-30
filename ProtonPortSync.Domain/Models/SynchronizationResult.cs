using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPortSync.Domain.Models
{
    public sealed record SynchronizationResult(bool WasUpdated, bool IsSynchronized, int? ProtonPort, int QbittorrentPortBefore, int QbittorrentPortAfter, DateTimeOffset SynchronizedAt)
    {
    }
}
