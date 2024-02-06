
using System.Text;

namespace ConsoleGame.Service
{
    public class OneMoveGameRulesService : IGameRules
    {
        const byte minGameMoves = 3;

        public Dictionary<string, Dictionary<string, object>>? GameRules { get; private set; }
        public string[]? GameArgs { get; private set; }

        public bool ValidateArgs(string[] gameArgs, out string errorMessage)
        {
            var sbMsg = new StringBuilder();
            var validateResult = true;

            if (gameArgs.Distinct().Count() != gameArgs.Length) sbMsg.AppendLine("All game moves must be unique;");
            if (gameArgs.Length % 2 == 0) sbMsg.AppendLine("The number of game moves must be odd;");
            if (gameArgs.Length < minGameMoves) sbMsg.AppendLine("The number of game moves must be >= " + minGameMoves + ";");

            if (sbMsg.Length > 0)
            {
                sbMsg.Insert(0, "Invalid input data:\n");
                validateResult = false;
            }

            errorMessage = sbMsg.ToString();
            return validateResult;
        }

        public string GetMenu()
        {
            var separator = " - ";

            var sbMenu = new StringBuilder("Available moves:\n");
            foreach (var arg in GameArgs)
            {
                var menuNumber = Array.IndexOf(GameArgs, arg) + 1;
                sbMenu.AppendLine(menuNumber + separator + arg);
            }
            sbMenu.AppendLine(0 + separator + "exit");
            sbMenu.AppendLine("?" + separator + "help");

            return sbMenu.ToString();
        }

        public Dictionary<string, Dictionary<string, object>> InitRules(string[] gameArgs)
        {
            GameRules = new Dictionary<string, Dictionary<string, object>>();
            GameArgs = gameArgs;
            var countWinsAndLoses = gameArgs.Length / 2;

            foreach (var arg in gameArgs)
            {
                var ruleForOneMove = GetRulesForOneMove(arg, gameArgs, countWinsAndLoses);
                GameRules.Add(arg, ruleForOneMove);
            }

            return GameRules;
        }

        private Dictionary<string, object> GetRulesForOneMove(string move, string[] allMoves, int countWinsAndLoses)
        {
            var moveRules = new Dictionary<string, object>();

            foreach (var m in allMoves)
            {
                //discord hint: Math.sign((a - b + p + n) % n - p); where a и b — move indiсes, n — moves number, p — is Math.floor(n / 2) or (n >> 1):
                var battleResult = Math.Sign((Array.IndexOf(allMoves, m) - Array.IndexOf(allMoves, move) + countWinsAndLoses + allMoves.Length) % allMoves.Length - countWinsAndLoses);
                switch (battleResult)
                {
                    case 0:
                        moveRules.Add(m, "Draw");
                        break;
                    case 1:
                        moveRules.Add(m, "Win");
                        break;
                    case -1:
                        moveRules.Add(m, "Lose");
                        break;
                }
            }
            return moveRules;
        }

        public string? Battle(string moveUser1, string moveUser2)
        {
            var battleResult = GameRules?.GetValueOrDefault(moveUser1)?.GetValueOrDefault(moveUser2);
            return battleResult?.ToString();
        }
    }

    public interface IGameRules
    {
        bool ValidateArgs(string[] gameMoves, out string errorMessage);
        Dictionary<string, Dictionary<string, object>> InitRules(string[] allMoves);
        string GetMenu();
        string? Battle(string moveUser1, string moveUser2);
    }
}
