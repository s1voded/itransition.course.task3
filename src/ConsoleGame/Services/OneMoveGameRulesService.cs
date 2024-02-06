
using System.Text;

namespace ConsoleGame.Service
{
    public class OneMoveGameRulesService: IGameRules
    {
        const byte minGameMoves = 3;

        public bool ValidateArgs(string[] gameMoves, out string errorMessage)
        {
            var sbMsg = new StringBuilder();
            var validateResult = true;

            if (gameMoves.Distinct().Count() != gameMoves.Length) sbMsg.AppendLine("All game moves must be unique;");
            if (gameMoves.Length % 2 == 0) sbMsg.AppendLine("The number of game moves must be odd;");
            if (gameMoves.Length < minGameMoves) sbMsg.AppendLine("The number of game moves must be >=").Append(minGameMoves).Append(';');

            if (sbMsg.Length > 0)
            {
                sbMsg.Insert(0,"Invalid input data:\n");
                validateResult = false;
            }

            errorMessage = sbMsg.ToString();
            return validateResult;
        }

        public string GetMenu(string[] gameMoves)
        {
            var separator = " - ";

            var sbMenu = new StringBuilder("Available moves:\n");
            foreach (var move in gameMoves)
            {
                var menuNumber = Array.IndexOf(gameMoves, move) + 1;
                sbMenu.AppendLine(menuNumber + separator + move);
            }
            sbMenu.AppendLine(0 + separator + "exit");
            sbMenu.AppendLine("?" + separator + "help");

            return sbMenu.ToString();
        }

        public Dictionary<string, Dictionary<string, object>> GetRules(string[] allMoves)
        {
            var movesRule = new Dictionary<string, Dictionary<string, object>>();

            foreach (var move in allMoves)
            {
                var ruleForOneMove = GetRulesForOneMove(move, allMoves);
                movesRule.Add(move, ruleForOneMove);
            }

            return movesRule;
        }

        private Dictionary<string, object> GetRulesForOneMove(string move, string[] allMoves)
        {
            var moveRules = new Dictionary<string, object>();
            var countWinsAndLoses = allMoves.Length / 2;

            foreach (var m in allMoves)
            {
                var sparingResult = Array.IndexOf(allMoves, m) - Array.IndexOf(allMoves, move);
                var sparingResultString = "Draw";
                if (sparingResult > 0)
                {
                    if (sparingResult <= countWinsAndLoses) sparingResultString = "Win";
                    else sparingResultString = "Lose";
                }
                if (sparingResult < 0)
                {
                    if (Math.Abs(sparingResult) <= countWinsAndLoses) sparingResultString = "Lose";
                    else sparingResultString = "Win";
                }

                moveRules.Add(m, sparingResultString);
            }

            return moveRules;
        }
    }

    public interface IGameRules
    {
        bool ValidateArgs(string[] gameMoves, out string errorMessage);
        string GetMenu(string[] gameMoves);
        Dictionary<string, Dictionary<string, object>> GetRules(string[] allMoves);
    }
}
