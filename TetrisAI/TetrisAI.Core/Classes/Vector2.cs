﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAI.Core.Classes
{
    public class Vector2
    {
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
