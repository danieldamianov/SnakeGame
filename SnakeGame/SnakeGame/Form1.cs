using SnakeGame.Enums;
using SnakeGame.SnakeFunctionality;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Snake snake;
        private Direction direction;

        private List<Button> snakeButtons;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.snake = new Snake();
            this.direction = Direction.Right;
            this.snake.snakePartsLocations[1].direction = Enums.Direction.Right;
            this.snakeButtons = new List<Button>();
            this.snakeButtons.Add(new Button());
            this.snakeButtons.Add(new Button());
            this.snakeButtons[0].Size = new Size(30, 30);
            this.snakeButtons[0].Location = new Point(0, 0);
            this.snakeButtons[0].Enabled = false;
            this.snakeButtons[1].Size = new Size(30, 30);
            this.snakeButtons[1].Location = new Point(30, 0);
            this.snakeButtons[1].Enabled = false;
            this.Controls.AddRange(this.snakeButtons.ToArray());
            this.timer1.Interval = 100;
            this.timer1.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    if (this.direction != Direction.Up)
                    {
                        this.direction = Direction.Down; 
                    }
                    break;
                case Keys.Up:
                    if (this.direction != Direction.Down)
                    {
                        this.direction = Direction.Up; 
                    }
                    break;
                case Keys.Right:
                    if (this.direction != Direction.Left)
                    {
                        this.direction = Direction.Right;
                    }
                    break;
                case Keys.Left:
                    if (this.direction != Direction.Right)
                    {
                        this.direction = Direction.Left;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        int counter = 0;
        private void MoveSnake()
        {
            counter++;
            if (counter % 5 == 0)
            {
                this.snake.snakePartsLocations.Insert(0, new SnakePart
                (new Point(this.snake.snakePartsLocations[0].point.X, this.snake.snakePartsLocations[0].point.Y)));

                this.snakeButtons.Insert(0, new Button());
                this.snakeButtons[0].Size = new Size(30, 30);
                this.Controls.Add(snakeButtons[0]);
                //this.snakeButtons[0].Location = new Point(0, 0);
                this.snakeButtons[0].Enabled = false;
            }
            
            var head = this.snake.snakePartsLocations.Last();
            switch (direction)
            {

                case Direction.Up:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X, head.point.Y - 30);
                    break;
                case Direction.Down:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X, head.point.Y + 30);
                    break;
                case Direction.Left:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X - 30, head.point.Y);
                    break;
                case Direction.Right:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X + 30, head.point.Y);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < this.snakeButtons.Count; i++)
            {
                this.snakeButtons[i].Location = new Point(this.snake.snakePartsLocations[i].point.X, this.snake.snakePartsLocations[i].point.Y);
            }

        }
        private void MoveBody()
        {
            for (int i = 0; i < this.snake.snakePartsLocations.Count - 1; i++)
            {
                this.snake.snakePartsLocations[i].point = this.snake.snakePartsLocations[i + 1].point;
            }
        }
    }
}

