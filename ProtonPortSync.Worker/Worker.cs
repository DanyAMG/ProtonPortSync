using ProtonPortSync.Application.UseCases;

namespace ProtonPortSync.Worker
{
    public class Worker : BackgroundService
    {
        private readonly SynchronizePortsUseCase _syncPortsUseCase;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, SynchronizePortsUseCase syncPortsUseCase)
        {
            _logger = logger;
            _syncPortsUseCase = syncPortsUseCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = await _syncPortsUseCase.ExecuteAsync(stoppingToken);
                    _logger.LogInformation("Synchronized successfully: {@Result}", result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while synchronizing ports.");
                }
                
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
