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
        private List<Vector2> _nextPieces;
        private List<Vector2> _passivePieces;

        private Random _random;

        private double _time;
        private double _speed;

        private int _score;

        internal Game()
        {
            _gameState = GameState.NotInitialized;
        }

        public void StartGame(int x, int y)
        {
            _size = new Vector2(x, y);
            _random = new Random(DateTime.Now.Millisecond);

            Init();
        }

        private void Init()
        {
            _score = 0;
            _time = 0;
            _speed = 2000;
            _gameState = GameState.Playing;
            var piece = _random.Next(1, 8);
            var generatedPieces = PieceList.GetPiece(piece);
            _activePieces = new List<Vector2>();
            _nextPieces = new List<Vector2>();
            _passivePieces = new List<Vector2>();
            foreach (var generatedPiece in generatedPieces)
            {
                _activePieces.Add(new Vector2((generatedPiece.X + (_size.X - generatedPieces.Max(g => g.X)) / 2), generatedPiece.Y - 1));
            }
            piece = _random.Next(1, 7);
            generatedPieces = PieceList.GetPiece(piece);
            foreach (var generatedPiece in generatedPieces)
            {
                _nextPieces.Add(new Vector2(generatedPiece.X, generatedPiece.Y));
            }
        }

        public List<Vector2> GetPieces()
        {
            var list = new List<Vector2>();
            foreach(var activePiece in _activePieces.Where(a => a.Y >= 0))
            {
                list.Add(new Vector2(activePiece.X, activePiece.Y));
            }
            foreach (var passivePiece in _passivePieces.Where(a => a.Y >= 0))
            {
                list.Add(new Vector2(passivePiece.X, passivePiece.Y));
            }
            return list;
        }

        public void MoveDown()
        {
            if (_gameState != GameState.Playing)
                return;
            bool isPossible = true;
            foreach (var activePiece in _activePieces)
            {
                if (activePiece.Y == _size.Y - 1)
                {
                    isPossible = false;
                }
                if (_passivePieces.Any(p => p.X == activePiece.X && p.Y == activePiece.Y + 1))
                {
                    isPossible = false;
                }
            }
            if (isPossible)
            {
                foreach (var activePiece in _activePieces)
                {
                    activePiece.Y += 1;
                }
                _score += 1;
            }
            else
            {
                if(_time < _speed)
                    _time = _speed;
            }
        }

        public void MoveLeft()
        {
            if (_gameState != GameState.Playing)
                return;
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
            if (_gameState != GameState.Playing)
                return;
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
            if (_gameState != GameState.Playing)
                return;
            var tmp = new List<Vector2>();
            var minx = _activePieces.Min(a => a.X);
            var maxx = _activePieces.Max(a => a.X);
            var miny = _activePieces.Min(a => a.Y);
            var maxy = _activePieces.Max(a => a.Y);
            var px = minx + ((maxx - minx) / 2);
            var py = miny + ((maxy - miny) / 2);
            foreach (var activePiece in _activePieces)
            {
                tmp.Add(new Vector2(activePiece.X - px, activePiece.Y - py));
            }
            bool isPossible = true;
            foreach(var t in tmp)
            {
                var ox = t.X;
                var oy = t.Y;

                t.X = -oy + px;
                t.Y = ox + py;

                if (t.X >= _size.X)
                {
                    isPossible = false;
                }
                if (t.X < 0)
                {
                    isPossible = false;
                }
                if (t.Y >= _size.Y)
                {
                    isPossible = false;
                }
                if (_passivePieces.Any(p => p.X == t.X && p.Y == t.Y))
                {
                    isPossible = false;
                }

            }
            if (isPossible)
            {
                _activePieces = new List<Vector2>();
                foreach (var t in tmp)
                {
                    _activePieces.Add(new Vector2(t.X, t.Y));
                }
            }

            }

            public void Update(double delta)
        {
            if (_gameState == GameState.GameOverFilling)
            {
                _time += delta;
                while (_time > _speed)
                {
                    _time -= _speed;
                    if(_passivePieces.Count >= _size.X * _size.Y)
                    {
                        _gameState = GameState.GameOverEmptying;
                    }
                    bool breakout = false;
                    for (var y = _size.Y - 1; y >= 0; y--)
                    {
                        for (var x = 0; x < _size.X; x++)
                        {
                            if (!_passivePieces.Any(p => p.X == x && p.Y == y))
                            {
                                _passivePieces.Add(new Vector2(x, y));
                                breakout = true;
                                break;
                            }
                        }
                        if (breakout)
                            break;
                    }
                }
            }
            if (_gameState == GameState.GameOverEmptying)
            {
                _time += delta;
                while (_time > _speed)
                {
                    _time -= _speed;
                    if (_passivePieces.Count == 0)
                    {
                        Init();
                    }
                    else
                    {
                        var piece = _passivePieces.OrderByDescending(p => p.X).OrderBy(p => p.Y).First();

                        _passivePieces.Remove(piece);
                    }
                }
            }
            if (_gameState != GameState.Playing)
                return;
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
                    int scoredLines = 0;
                    for (int y = 0; y < _size.Y; y++)
                    {
                        if (_passivePieces.Count(p => p.Y == y) == _size.X)
                        {
                            scoredLines++;
                            //Remove line
                            foreach (var passivePiece in _passivePieces.ToList())
                            {
                                if (passivePiece.Y == y)
                                    _passivePieces.Remove(passivePiece);
                            }
                            //Drop all pieces above
                            foreach (var passivePiece in _passivePieces.ToList())
                            {
                                if(passivePiece.Y < y)
                                {
                                    passivePiece.Y += 1;
                                }
                            }
                        }
                    }
                    if(scoredLines == 1)
                        _score += 100;
                    if (scoredLines == 2)
                        _score += 250;
                    if (scoredLines == 3)
                        _score += 500;
                    if (scoredLines == 4)
                        _score += 1000;
                    var piece = _random.Next(1, 8);
                    var generatedPieces = PieceList.GetPiece(piece);
                    _activePieces = new List<Vector2>();
                    foreach (var nextPiece in _nextPieces)
                    {
                        _activePieces.Add(new Vector2((nextPiece.X + (_size.X - generatedPieces.Max(g => g.X)) / 2), nextPiece.Y - 1));
                    }
                    foreach(var activePiece in _activePieces)
                    {
                        if(_passivePieces.Any(p => p.X == activePiece.X && p.Y == activePiece.Y))
                        {
                            _gameState = GameState.GameOverFilling;
                            _speed = 100;
                            _activePieces = new List<Vector2>();
                        }
                    }
                    _nextPieces = new List<Vector2>();
                    foreach (var generatedPiece in generatedPieces)
                    {
                        _nextPieces.Add(new Vector2(generatedPiece.X, generatedPiece.Y));
                    }

                }
            }
        }

        public int GetScore()
        {
            return _score;
        }

        public List<Vector2> GetNextPieces()
        {
            return _nextPieces;
        }
    }
}
