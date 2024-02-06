namespace ConsoleGame.Services
{
    public class ConsoleWriterService : IMessageWriter
    {
        private ITableGenerator _tableGenerator;

        public ConsoleWriterService(ITableGenerator tableGenerator)
        {
            _tableGenerator = tableGenerator;
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteTable(Dictionary<string, Dictionary<string, object>>? table)
        {
            if (table != null)
                Write(_tableGenerator.GenerateTable(table));
        }
    }

    public interface IMessageWriter
    {
        void Write(string message);
        void WriteTable(Dictionary<string, Dictionary<string, object>>? table);
    }
}
