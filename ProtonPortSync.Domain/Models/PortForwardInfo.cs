using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPortSync.Domain.Models
{
    public sealed record PortForwardInfo(int Port, DateTimeOffset DetectedAt)
    {
    }
}
