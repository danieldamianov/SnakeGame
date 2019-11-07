using SnakeGame.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeFunctionality
{
    public class SnakePart
    {
        public Point point;

        public SnakePart(Point point)
        {
            this.point = point;
        }

        public Direction direction;
    }
}
