using ConsoleTables;

namespace ConsoleGame.Services
{
    public class ConsoleTableGeneratorService : ITableGenerator
    {
        public string GenerateTable(Dictionary<string, Dictionary<string, object>> tableData)
        {
            var table = ConsoleTable.FromDictionary(tableData);
            table.Options.EnableCount = false;
            table.Columns[0] = "PC/User >";

            return table.ToString();
        }
    }

    public interface ITableGenerator
    {
        string GenerateTable(Dictionary<string, Dictionary<string, object>> data);
    }
}
