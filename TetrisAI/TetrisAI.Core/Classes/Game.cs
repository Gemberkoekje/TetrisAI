using System;
using System.Collections.Generic;
using System.Text;
using TetrisAI.Core.Enums;
using TetrisAI.Core.Interfaces;

namespace TetrisAI.Core.Classes
{
    internal class Game : IGame
    {
        private GameState _gameState;

        Game()
        {
            _gameState = GameState.NotInitialized;
        }

        public void StartGame(int x, int y)
        {
            throw new NotImplementedException();
        }

        public List<Vector2> GetPieces()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void Plummet()
        {
            throw new NotImplementedException();
        }

        public void Rotate()
        {
            throw new NotImplementedException();
        }
    }
}
