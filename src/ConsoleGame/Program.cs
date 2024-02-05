using ConsoleGame.Service;
using ConsoleGame.Services;
using ConsoleTables;

ConsoleOutputService.ValidateArgs(args);

var key = CryptoService.GenerateKey();

Console.WriteLine("KEY: " + key);
Console.WriteLine("HMAC: " + CryptoService.GenerateHMAC("stone", key));

var rulesService = new RulesService();
var moveRules = rulesService.GetRules(args);

HelpTableService.ShowTable(moveRules);

var result = moveRules["a"]["c"];
Console.WriteLine(result);