
using System.Text;

namespace ConsoleGame.Service
{
    public class RulesService
    {
        const byte minGameMoves = 3;

        public bool ValidateMoves(string[] gameMoves, out string msg)
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

            msg = sbMsg.ToString();
            return validateResult;
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
}
