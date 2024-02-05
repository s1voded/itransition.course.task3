using ConsoleGame.Core;
using ConsoleGame.Service;
using ConsoleGame.Services;
using Microsoft.Extensions.DependencyInjection;


var services = CreateServices();

var game = services.GetRequiredService<GameCore>();
game.Play(args);

 static ServiceProvider CreateServices()
{
    var serviceProvider = new ServiceCollection()
        .AddScoped<GameCore>()
        .AddScoped<RulesService>()
        .AddScoped<CryptoService>()
        .AddScoped<UIService>()
        .AddScoped<HelpTableService>()
        .BuildServiceProvider();

    return serviceProvider;
}