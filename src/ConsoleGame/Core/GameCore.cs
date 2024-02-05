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
        private RulesService _rulesService;
        private CryptoService _cryptoService;
        private UIService _uiService;

        public GameCore(RulesService rulesService, CryptoService cryptoService, UIService uiService)
        {
            _rulesService = rulesService;
            _cryptoService = cryptoService;
            _uiService = uiService;
        }

        public void Play(string[] gameMoves)
        {
            if(!_rulesService.ValidateMoves(gameMoves, out string msg)) _uiService.ShowMsg(msg);
            else
            {
                var gameRules = _rulesService.GetRules(gameMoves);

                var random = new Random();
                var pc_move = gameMoves[random.Next(0, gameMoves.Length)];
                var key = _cryptoService.GenerateKey();
                var hmac = _cryptoService.GenerateHMAC(pc_move, key);

                _uiService.ShowMsg("HMAC: " + hmac);
                _uiService.ShowGameMenu(gameMoves);

                var user_move = gameMoves[random.Next(0, gameMoves.Length)];

                _uiService.ShowMsg("User move: " + user_move);
                _uiService.ShowMsg("Computer move: " + pc_move);

                var sparingResult = gameRules[user_move][pc_move].ToString();

                _uiService.ShowMsg(sparingResult);
                _uiService.ShowMsg("HMAC key: " + key);

            }

        }

    }
}
