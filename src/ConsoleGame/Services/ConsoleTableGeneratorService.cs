using ConsoleTables;

namespace ConsoleGame.Services
{
    public class ConsoleTableGeneratorService: ITableGenerator
    {
        public string GenerateTable(Dictionary<string, Dictionary<string, object>> moveRules)
        {
            var helpMoveRulesTable = ConsoleTable.FromDictionary(moveRules);
            helpMoveRulesTable.Options.EnableCount = false;

            return helpMoveRulesTable.ToString();
        }
    }

    public interface ITableGenerator
    {
        string GenerateTable(Dictionary<string, Dictionary<string, object>> data);
    }
}
