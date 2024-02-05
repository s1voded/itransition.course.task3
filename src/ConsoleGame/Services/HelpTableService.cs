using ConsoleTables;

namespace ConsoleGame.Services
{
    public class HelpTableService
    {
        public string GenerateHelpTable(Dictionary<string, Dictionary<string, object>> moveRules)
        {
            var helpMoveRulesTable = ConsoleTable.FromDictionary(moveRules);
            helpMoveRulesTable.Options.EnableCount = false;

            return helpMoveRulesTable.ToString();
        }
    }
}
