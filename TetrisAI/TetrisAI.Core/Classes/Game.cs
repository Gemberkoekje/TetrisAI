using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TetrisAI.Core.Enums;
using TetrisAI.Core.Interfaces;

namespace TetrisAI.Core.Classes
{
    internal class Game : IGame
    {
        private GameState _gameState;
        private Vector2 _size;

        private List<Vector2> _activePieces;
        private List<Vector2> _passivePieces;

        private Random _random;

        private double _time;
        private double _speed;

        internal Game()
        {
            _gameState = GameState.NotInitialized;
        }

        public void StartGame(int x, int y)
        {
            _time = 0;
            _speed = 2000;
            _size = new Vector2(x, y);
            _gameState = GameState.Playing;
            _random = new Random(DateTime.Now.Millisecond);

            var piece = _random.Next(1,7);
            var generatedPieces = PieceList.GetPiece(piece);
            _activePieces = new List<Vector2>();
            _passivePieces = new List<Vector2>();
            foreach (var generatedPiece in generatedPieces)
            {
                _activePieces.Add(new Vector2((generatedPiece.X + (_size.X - generatedPieces.Max(g => g.X)) / 2), generatedPiece.Y));
            }
        }

        public List<Vector2> GetPieces()
        {
            var list = new List<Vector2>();
            foreach(var activePiece in _activePieces)
            {
                list.Add(new Vector2(activePiece.X, activePiece.Y));
            }
            foreach (var passivePiece in _passivePieces)
            {
                list.Add(new Vector2(passivePiece.X, passivePiece.Y));
            }
            return list;
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            bool isPossible = true;
            foreach (var activePiece in _activePieces)
            {
                if (activePiece.X == 0)
                {
                    isPossible = false;
                }
                if (_passivePieces.Any(p => p.X == activePiece.X - 1 && p.Y == activePiece.Y))
                {
                    isPossible = false;
                }
            }
            if (isPossible)
            {
                foreach (var activePiece in _activePieces)
                {
                    activePiece.X -= 1;
                }
            }
        }

        public void MoveRight()
        {
            bool isPossible = true;
            foreach (var activePiece in _activePieces)
            {
                if (activePiece.X == _size.X - 1)
                {
                    isPossible = false;
                }
                if (_passivePieces.Any(p => p.X == activePiece.X + 1 && p.Y == activePiece.Y))
                {
                    isPossible = false;
                }
            }
            if (isPossible)
            {
                foreach (var activePiece in _activePieces)
                {
                    activePiece.X += 1;
                }
            }
        }

        public void Plummet()
        {
            throw new NotImplementedException();
        }

        public void Rotate()
        {
            throw new NotImplementedException();
        }

        public void Update(double delta)
        {
            _time += delta;
            while (_time > _speed)
            {
                _time -= _speed;
                bool becomePassive = false;
                foreach (var activePiece in _activePieces)
                {
                    if (activePiece.Y == _size.Y - 1)
                        becomePassive = true;
                    if (_passivePieces.Any(p => p.X == activePiece.X && p.Y == activePiece.Y + 1))
                        becomePassive = true;
                }
                if (!becomePassive)
                {
                    foreach (var activePiece in _activePieces)
                    {
                        activePiece.Y += 1;
                    }
                }
                else
                {
                    foreach (var activePiece in _activePieces)
                    {
                        _passivePieces.Add(new Vector2(activePiece.X, activePiece.Y));
                    }
                    var piece = _random.Next(1, 7);
                    var generatedPieces = PieceList.GetPiece(piece);
                    _activePieces = new List<Vector2>();
                    foreach (var generatedPiece in generatedPieces)
                    {
                        _activePieces.Add(new Vector2((generatedPiece.X + (_size.X - generatedPieces.Max(g => g.X)) / 2), generatedPiece.Y));
                    }

                }
            }
        }
    }
}
