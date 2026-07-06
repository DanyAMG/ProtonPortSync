using ProtonPortSync.Application.UseCases;
using ProtonPortSync.Worker;
using ProtonPortSync.Application.Abstractions;
using ProtonPortSync.Infrastructure.Fake;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<SynchronizePortsUseCase>();

//temporary workaround for test
builder.Services.AddSingleton<IProtonPortProvider, FakeProtonPortProvider>();
builder.Services.AddSingleton<IQbittorrentPortService, FakeQbittorrentPortService>();

var host = builder.Build();
host.Run();
