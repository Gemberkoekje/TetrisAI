using System;
using System.Collections.Generic;
using System.Text;
using TetrisAI.Core.Classes;
using TetrisAI.Core.Interfaces;

namespace TetrisAI.Core.Factories
{
    public class GameFactory
    {
        private Game _game;

        public GameFactory()
        {
            _game = new Game();
        }

        public IGame GetGame()
        {
            return _game;
        }
    }
}
