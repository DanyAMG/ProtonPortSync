using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPortSync.Domain.Models
{
    public sealed record QbittorrentConfiguration(string Host, string Username, string Password)
    {
    }
}
