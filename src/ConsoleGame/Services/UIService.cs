
using System.Text;

namespace ConsoleGame.Services
{
    public class UIService
    {
        public void ShowGameMenu(string[] gameMoves)
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

            ShowMsg(sbMenu.ToString());
        }

        public void UserMenuInput(string input)
        {
            if (int.TryParse(input, out int inputMuneNumber))
            {

            }
        }

        public void ShowMsg(string msg)
        { Console.WriteLine(msg); }
    }
}
