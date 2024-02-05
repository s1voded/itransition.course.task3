using ConsoleGame.Service;
using ConsoleTables;

var key = CryptoService.GenerateKey();

Console.WriteLine("KEY: " + key);
Console.WriteLine("HMAC: " + CryptoService.GenerateHMAC("stone", key));

var rulesService = new RulesService();
var moveRules = rulesService.GetRules(args);
var helpMoveRulesTable = ConsoleTable.FromDictionary(moveRules);
helpMoveRulesTable.Options.EnableCount = false;

Console.WriteLine(helpMoveRulesTable.ToString());

var result = moveRules["a"]["c"];
Console.WriteLine(result);