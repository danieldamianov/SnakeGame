using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeFunctionality
{
    public class Snake
    {
        public List<SnakePart> snakePartsLocations;
        public Snake()
        {
            snakePartsLocations = new List<SnakePart>();
            snakePartsLocations.Add(new SnakePart(new Point(0,0)));
            snakePartsLocations.Add(new SnakePart(new Point(GlobalConstraints.SnakeWidth, 0)));
        }
    }
}
