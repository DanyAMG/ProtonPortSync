using ProtonPortSync.Application.Abstractions;
using ProtonPortSync.Domain.Models;
using ProtonPortSync.Application.UseCases;

namespace ProtonPortSync.Tests
{
    public class SynchronizePortsUseCaseTests
    {
        private sealed class FakeProtonPortProvider : IProtonPortProvider
        {
            public int? CurrentPort { get; set; } = 12345;
            Task<PortForwardInfo?> IProtonPortProvider.GetCurrentPortAsync(CancellationToken cancellationToken)
            {
                if(CurrentPort == null)
                {
                    return Task.FromResult<PortForwardInfo?>(null);
                }
                return Task.FromResult<PortForwardInfo?>(new PortForwardInfo(CurrentPort, DateTimeOffset.UtcNow));
            }
        }

        private sealed class FakeQbittorrentPortService : IQbittorrentPortService
        {
            public int CurrentPort { get; set; } = 12345;
            Task<int> IQbittorrentPortService.GetCurrentPortAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(CurrentPort);
            }
            Task IQbittorrentPortService.UpdatePortAsync(int? newPort, CancellationToken cancellationToken)
            {
                CurrentPort = newPort ?? 0;
                return Task.CompletedTask;
            }
        }

        [Fact]
        public async Task ExecuteAsync_WhenPortsAreDifferent_UpdatesQBittorentPort()
        {
            // Arrange
            var qBittorrentPortService = new FakeQbittorrentPortService
            {
                CurrentPort = 54321
            };
            var protonPortProvider = new FakeProtonPortProvider
            {
                CurrentPort = 12345
            };
            var synchronizePortsUseCase = new SynchronizePortsUseCase(protonPortProvider, qBittorrentPortService);

            //Act
            var result = await synchronizePortsUseCase.ExecuteAsync(CancellationToken.None);

            //Assert
            Assert.True(result.WasUpdated,"Port was not updated");
            Assert.True(result.IsSynchronized, "Ports are not synchronized");
            Assert.Equal(54321, result.QbittorrentPortBefore);
            Assert.Equal(12345, result.QbittorrentPortAfter);
        }

        [Fact]
        public async Task ExecuteAsync_WhenPortsAreTheSame_DoesNotUpdatesQbittorentPort()
        {
            // Arrange
            var qBittorrentPortService = new FakeQbittorrentPortService
            {
                CurrentPort = 12345
            };
            var protonPortProvider = new FakeProtonPortProvider
            {
                CurrentPort = 12345
            };
            var synchronizePortsUseCase = new SynchronizePortsUseCase(protonPortProvider, qBittorrentPortService);

            //Act
            var result = await synchronizePortsUseCase.ExecuteAsync(CancellationToken.None);

            //Assert
            Assert.False(result.WasUpdated, "Port was updated");
            Assert.True(result.IsSynchronized, "Ports are not synchronized");
            Assert.Equal(12345, result.QbittorrentPortBefore);
            Assert.Equal(12345, result.QbittorrentPortAfter);
        }

        [Fact]
        public async Task ExecuteAsync_WhenNoProtonIsDetected_ThrowsInvalidOperationException()
        {
            // Arrange
            var protonPortProvider = new FakeProtonPortProvider
            {
                CurrentPort = null
            };


            var qBittorrentPortService = new FakeQbittorrentPortService
            {
                CurrentPort = 12345
            };

            //Act

            var result = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                var synchronizePortsUseCase = new SynchronizePortsUseCase(protonPortProvider, qBittorrentPortService);
                await synchronizePortsUseCase.ExecuteAsync(CancellationToken.None);
            });

            //Assert
            Assert.StartsWith("No Proton port was detected.",result.Message);
        }
    }
}
