using System.Text.Json.Serialization;

namespace ProtonPortSync.Infrastructure.Qbittorrent.ApiModels
{
    internal sealed class QbittorrentPreferencesResponse
    {
        [JsonPropertyName("listen_port")]
        public int ListenPort { get; init; }
    }
}
