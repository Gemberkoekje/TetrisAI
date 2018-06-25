using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAI.Core.Classes
{

    internal static class PieceList
    {
        internal static List<Vector2> GetPiece(int type)
        {
            if (type < 1 || type > 7)
                throw new ArgumentOutOfRangeException("type", type, "between 1 and 6");
            var list = new List<Vector2>();

            if(type == 1)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(2, 0));
                list.Add(new Vector2(3, 0));
            }
            if (type == 2)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(2, 0));
                list.Add(new Vector2(2, 1));
            }
            if (type == 3)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(0, 1));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(2, 0));
            }
            if (type == 4)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(1, 1));
                list.Add(new Vector2(2, 1));
            }
            if (type == 5)
            {
                list.Add(new Vector2(0, 1));
                list.Add(new Vector2(1, 1));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(2, 0));
            }
            if (type == 6)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(2, 0));
                list.Add(new Vector2(1, 1));
            }
            if (type == 7)
            {
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(0, 1));
                list.Add(new Vector2(1, 1));
            }
            return list;


        }
    }
}
