using ConsoleGame.Services;

var key = CryptoService.GenerateKey();

Console.WriteLine("KEY: " + key);
Console.WriteLine("HMAC: " + CryptoService.GenerateHMAC("stone", key));
