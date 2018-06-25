using System;
using System.Collections.Generic;
using System.Text;
using TetrisAI.Core.Classes;

namespace TetrisAI.Core.Interfaces
{
    public interface IGame
    {

        void StartGame(int x, int y);

        List<Vector2> GetPieces();

        void MoveLeft();
        void MoveRight();
        void Rotate();
        void MoveDown();
        void Plummet();
    }
}
