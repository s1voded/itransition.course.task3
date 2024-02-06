
using System.Text;

namespace ConsoleGame.Services
{
    public class ConsoleWriterService: IMessageWriter
    {
        

        public void UserMenuInput(string input)
        {
            if (int.TryParse(input, out int inputMuneNumber))
            {

            }
        }

        public void Write(string message)
        { 
            Console.WriteLine(message); 
        }
    }

    public interface IMessageWriter
    {
        void Write(string message);
    }
}
