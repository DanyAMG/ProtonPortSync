using System;
using System.Collections.Generic;
using System.Text;
using ProtonPortSync.Domain.Models;

namespace ProtonPortSync.Application.Abstractions
{
    public interface IProtonPortProvider
    {
        Task<PortForwardInfo?> GetCurrentPortAsync(CancellationToken cancellationToken);
    }
}
