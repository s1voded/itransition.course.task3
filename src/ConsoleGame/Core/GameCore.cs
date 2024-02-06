using ConsoleGame.Service;
using ConsoleGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Core
{
    public class GameCore
    {
        private IGameRules _rulesService;
        private IHMACGenerator _hmacService;
        private IMessageWriter _messageWriterService;

        public GameCore(IGameRules rulesService, IHMACGenerator hmacService, IMessageWriter messageWriterService)
        {
            _rulesService = rulesService;
            _hmacService = hmacService;
            _messageWriterService = messageWriterService;
        }

        public void Play(string[] gameMoves)
        {
            if(_rulesService.ValidateArgs(gameMoves, out string errorMessage))
            {
                var gameRules = _rulesService.GetRules(gameMoves);

                var random = new Random();
                var pc_move = gameMoves[random.Next(0, gameMoves.Length)];
                var key = _hmacService.GenerateKey();
                var hmac = _hmacService.GenerateHMAC(pc_move, key);

                _messageWriterService.Write("HMAC: " + hmac);
                var menu = _rulesService.GetMenu(gameMoves);
                _messageWriterService.Write(menu);

                var user_move = gameMoves[random.Next(0, gameMoves.Length)];

                _messageWriterService.Write("User move: " + user_move);
                _messageWriterService.Write("Computer move: " + pc_move);

                var sparingResult = gameRules[user_move][pc_move].ToString();

                _messageWriterService.Write(sparingResult);
                _messageWriterService.Write("HMAC key: " + key);
            }
            else _messageWriterService.Write(errorMessage);

        }

    }
}
