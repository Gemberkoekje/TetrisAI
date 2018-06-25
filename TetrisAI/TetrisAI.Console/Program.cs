using System;
using TetrisAI.Core.Factories;

namespace TetrisAI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            var gameFactory = new GameFactory();
            var game = gameFactory.GetGame();
            var x = 7;
            var y = 11;
            game.StartGame(x,y);
        }
    }
}
