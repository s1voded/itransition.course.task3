using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Services
{
    public class ConsoleOutputService
    {
        public static void ValidateArgs(string[] gameMoves)
        {
            string validateMessage = "";

            if (gameMoves.Distinct().Count() != gameMoves.Length) validateMessage += "\nAll game moves must be unique;";
            if (gameMoves.Length % 2 == 0) validateMessage += "\nThe number of game moves must be odd;";
            if (gameMoves.Length < 3) validateMessage += "\nThe number of game moves must be >= 3;";

            if (!string.IsNullOrEmpty(validateMessage))
            {
                Console.WriteLine("Invalid input data:" + validateMessage);
                Environment.Exit(0);
            }
        }
    }
}
