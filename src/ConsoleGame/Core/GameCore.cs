using ConsoleGame.Service;
using ConsoleGame.Services;

namespace ConsoleGame.Core
{
    public class GameCore
    {
        private IGameRules _rulesService;
        private IHMACGenerator _hmacService;
        private IMessageWriter _messageWriterService;

        public Dictionary<string, Dictionary<string, object>>? GameRules { get; private set; }
        public string[]? GameMoves { get; private set; }

        public GameCore(IGameRules rulesService, IHMACGenerator hmacService, IMessageWriter messageWriterService)
        {
            _rulesService = rulesService;
            _hmacService = hmacService;
            _messageWriterService = messageWriterService;
        }

        public void Play(string[] gameMoves)
        {
            //1. validate input args
            if (_rulesService.ValidateArgs(gameMoves, out string errorMessage))
            {
                //2. generate game rules
                GameRules = _rulesService.InitRules(gameMoves);
                GameMoves = gameMoves;

                //3. generate random pc move
                var pc_move = PCMove();

                //4. generate key and hmac based on move
                var key = _hmacService.GenerateKey();
                var hmac = _hmacService.GenerateHMAC(pc_move, key);
                _messageWriterService.Write("HMAC: " + hmac);

                //5. user input (recursive method. the value is returned only when the user selects a move)
                var user_move = UserInputAndMove();

                //6. show moves and start battle
                _messageWriterService.Write("Your move: " + user_move + "\nComputer move: " + pc_move);
                var battleResult = _rulesService.Battle(user_move, pc_move);

                //7. show battle result and hmac key
                if (battleResult != "Draw") battleResult = "You " + battleResult;
                _messageWriterService.Write(battleResult + "\n" + "HMAC key: " + key);

                //8.exit
                Environment.Exit(0);
            }
            else _messageWriterService.Write(errorMessage);
        }

        private string UserInputAndMove()
        {
            _messageWriterService.Write(_rulesService.GetMenu());
            _messageWriterService.Write("Enter your move: ");

            var user_input = Console.ReadLine();
            if (int.TryParse(user_input, out int moveInt))
            {
                switch (moveInt)
                {
                    case 0://exit
                        Environment.Exit(0);
                        break;
                    case > 0 when moveInt <= GameMoves?.Length://user move
                        var user_move = GameMoves[moveInt - 1];
                        return user_move;
                    default:
                        _messageWriterService.Write("Please enter correct data:");
                        break;
                }
            }
            else if (user_input == "?") _messageWriterService.WriteTable(GameRules);//show help
            else _messageWriterService.Write("Please enter correct data:");

            return UserInputAndMove();//if the user enter not move, start again
        }

        private string PCMove()
        {
            var random = new Random();
            var pc_move = GameMoves?[random.Next(0, GameMoves.Length)];

            return pc_move;
        }

    }
}
