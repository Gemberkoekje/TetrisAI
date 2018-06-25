﻿using System;
using System.Diagnostics;
using System.Timers;
using TetrisAI.Core.Factories;
using TetrisAI.Core.Interfaces;

namespace TetrisAI.ConsoleApp
{
    class Program
    {
        static IGame _game;
        static int _width;
        static int _height;
        static Stopwatch _stopwatch;
        static double _lastElapsed;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var gameFactory = new GameFactory();
            _game = gameFactory.GetGame();
            _width = 7;
            _height = 11;
            _game.StartGame(_width, _height);
            Timer timer = new Timer();
            timer.Interval = (1000); // 1 secs
            timer.Elapsed += timer_Tick;
            timer.Start();
            _stopwatch = Stopwatch.StartNew();
            _lastElapsed = 0;
            var quit = false;
            while(!quit)
            {
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.LeftArrow)
                {
                    _game.MoveLeft();
                    Update();
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    _game.MoveRight();
                    Update();
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    _game.Rotate();
                    Update();
                }

            }
        }
        private static void timer_Tick(object sender, EventArgs e)
        {
            //refresh here...
            Update();
        }

        private static void Update()
        {
            _game.Update(_stopwatch.Elapsed.TotalMilliseconds - _lastElapsed);
            _lastElapsed = _stopwatch.Elapsed.TotalMilliseconds;
            Console.Clear();
            for (int x = 1; x < _width * 2; x += 2)
            {
                for (int y = 1; y < _height * 2; y += 2)
                {
                    DrawBox(x, y);
                }
            }
            var pieces = _game.GetPieces();
            foreach (var piece in pieces)
            {
                DrawBlock(piece.X * 2 + 1, piece.Y * 2 + 1);
            }
        }

        static void DrawBox(int x, int y)
        {
            Console.CursorTop = y - 1;
            Console.CursorLeft = x - 1;
            Console.Write("┼─┼");
            Console.CursorTop = y;
            Console.CursorLeft = x - 1;
            Console.Write("│ │");
            Console.CursorTop = y + 1;
            Console.CursorLeft = x - 1;
            Console.Write("┼─┼");
        }
        static void DrawBlock(int x, int y)
        {
            Console.CursorTop = y;
            Console.CursorLeft = x;
            Console.Write("█");

        }
    }
}
