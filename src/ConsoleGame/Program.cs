using ConsoleGame.Core;
using ConsoleGame.Service;
using ConsoleGame.Services;
using Microsoft.Extensions.DependencyInjection;


var services = CreateServices();

services.GetRequiredService<GameCore>().Play(args);

 static ServiceProvider CreateServices()
{
    var serviceProvider = new ServiceCollection()
        .AddScoped<GameCore>()
        .AddScoped<IGameRules, OneMoveGameRulesService>()
        .AddScoped<IHMACGenerator, SHA256HMACService>()
        .AddScoped<IMessageWriter, ConsoleWriterService>()
        .AddScoped<ITableGenerator, ConsoleTableGeneratorService>()
        .BuildServiceProvider();

    return serviceProvider;
}