using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Service
{
    internal class RulesService
    {
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
