using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Services
{
    public class HelpTableService
    {
        public static void ShowTable(Dictionary<string, Dictionary<string, object>> moveRules)
        {
            var helpMoveRulesTable = ConsoleTable.FromDictionary(moveRules);
            helpMoveRulesTable.Options.EnableCount = false;

            Console.WriteLine(helpMoveRulesTable.ToString());
        }
    }
}
