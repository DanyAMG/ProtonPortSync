using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPortSync.Infrastructure.Configuration
{
    public sealed class QbittorrentOptions
    {
        public const string SectionName = "Qbittorrent";

        public required string BaseUrl { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
