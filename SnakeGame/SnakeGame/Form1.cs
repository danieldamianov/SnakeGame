using SnakeGame.Enums;
using SnakeGame.SnakeFunctionality;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private Button food;

        private bool hasReceivedDirectionInTheInterval;

        private List<Button> snakeButtons;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.hasReceivedDirectionInTheInterval = false;
            this.ClientSize = new Size(1000, 1000);
            this.food = new Button();
            this.panel1.ClientSize = new Size(690, 690);
            this.panel1.BackColor = Color.DarkGreen;
            
            this.food.Size = new Size(GlobalConstraints.SnakeWidth,GlobalConstraints.SnakeWidth);
            this.food.Location = new Point(new Random().Next(50, GlobalConstraints.FormSize - GlobalConstraints.SnakeWidth - 50),
                new Random().Next(50, GlobalConstraints.FormSize - GlobalConstraints.SnakeWidth - 50));
            //this.food.Location = new Point(470, 470);
            this.panel1.Controls.Add(this.food);
            this.snake = new Snake();
            this.direction = Direction.Right;
            this.snake.snakePartsLocations[1].direction = Enums.Direction.Right;
            this.snakeButtons = new List<Button>();
            this.snakeButtons.Add(new Button());
            this.snakeButtons.Add(new Button());
            this.snakeButtons[0].Size = new Size(GlobalConstraints.SnakeWidth, GlobalConstraints.SnakeWidth);
            this.snakeButtons[0].Location = new Point(0, 0);
            this.snakeButtons[0].Enabled = false;
            this.snakeButtons[1].Size = new Size(GlobalConstraints.SnakeWidth,GlobalConstraints.SnakeWidth);
            this.snakeButtons[1].Location = new Point(GlobalConstraints.SnakeWidth, 0);
            this.snakeButtons[1].Enabled = false;
            this.snakeButtons[1].BackColor = Color.Black;
            this.panel1.Controls.AddRange(this.snakeButtons.ToArray());
            this.timer1.Interval = 100;
            this.timer1.Start();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (hasReceivedDirectionInTheInterval == true)
            {
                return true;
            }
            switch (keyData)
            {
                case Keys.Down:
                    if (this.direction != Direction.Up)
                    {
                        this.direction = Direction.Down;
                        this.hasReceivedDirectionInTheInterval = true;
                    }
                    break;
                case Keys.Up:
                    if (this.direction != Direction.Down)
                    {
                        this.direction = Direction.Up;
                        this.hasReceivedDirectionInTheInterval = true;
                    }
                    break;
                case Keys.Right:
                    if (this.direction != Direction.Left)
                    {
                        this.direction = Direction.Right;
                        this.hasReceivedDirectionInTheInterval = true;
                    }
                    break;
                case Keys.Left:
                    if (this.direction != Direction.Right)
                    {
                        this.direction = Direction.Left;
                        this.hasReceivedDirectionInTheInterval = true;
                    }
                    break;
            }
            return true;
            //return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.hasReceivedDirectionInTheInterval = false;
            MoveSnake();
            CheckForEatingItself();
        }

        private void CheckForEatingItself()
        {
            var head = this.snakeButtons.Last();
            
            var body = this.snakeButtons.Take(this.snakeButtons.Count - 1);
            
            if (body.Any(p => this.TwoButtonsOverlap(p, head)))
            {
                this.timer1.Stop();
                MessageBox.Show("You lost!");
            }
        }

        private bool TwoButtonsOverlap(Button button1,Button button2)
        {
            return DoOverlap(new Point(button1.Location.X, button1.Location.Y), new Point(button1.Location.X + 30, button1.Location.Y + 30),
                new Point(button2.Location.X, button2.Location.Y), new Point(button2.Location.X + 30, button2.Location.Y + 30));
        }
        private bool DoOverlap(Point l1, Point r1,
                          Point l2, Point r2)
        {
            // If one rectangle is on left side of other  
            if (l1.X >= r2.X || l2.X >= r1.X)
            {
                return false;
            }

            // If one rectangle is above other 
            if (l1.Y >= r2.Y || l2.Y >= r1.Y) return false;
            //if (l1.Y < r2.Y || l2.Y < r1.Y)
            //{
            //    return false;
            //}
            return true;
        }
        int counter = 0;
        private void MoveSnake()
        {
            counter++;
            if (CheckForEating())
            {
                this.snake.snakePartsLocations.Insert(0, new SnakePart
                (new Point(this.snake.snakePartsLocations[0].point.X, this.snake.snakePartsLocations[0].point.Y)));

                this.snakeButtons.Insert(0, new Button());
                this.snakeButtons[0].Size = new Size(GlobalConstraints.SnakeWidth, GlobalConstraints.SnakeWidth);
                this.panel1.Controls.Add(snakeButtons[0]);
                //this.snakeButtons[0].Location = new Point(0, 0);
                this.snakeButtons[0].Enabled = false;
                Random random = new Random();
                this.food.Location = new Point(random.Next(50, GlobalConstraints.FormSize - GlobalConstraints.SnakeWidth - 50),
                random.Next(50, GlobalConstraints.FormSize - GlobalConstraints.SnakeWidth - 50));
            }
            
            var head = this.snake.snakePartsLocations.Last();
            switch (direction)
            {

                case Direction.Up:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X, head.point.Y - GlobalConstraints.SnakeWidth);
                    break;
                case Direction.Down:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X, head.point.Y + GlobalConstraints.SnakeWidth);
                    break;
                case Direction.Left:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X - GlobalConstraints.SnakeWidth, head.point.Y);
                    break;
                case Direction.Right:
                    MoveBody();
                    //var head = this.snake.snakePartsLocations.Last();
                    head.point = new Point(head.point.X + GlobalConstraints.SnakeWidth, head.point.Y);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < this.snakeButtons.Count; i++)
            {
                this.snakeButtons[i].Location = new Point(this.snake.snakePartsLocations[i].point.X, this.snake.snakePartsLocations[i].point.Y);
                if (this.IsOutOfTheBoard(this.snakeButtons[i]))
                {
                    this.snakeButtons[i].Location = this.GetLocationPassedThroughWall(this.snakeButtons[i].Location);
                    this.snake.snakePartsLocations[i].point = this.snakeButtons[i].Location;
                }
            }

        }

        private Point GetLocationPassedThroughWall(Point location)
        {
            if (location.X < 0)
            {
                return new Point(660, location.Y);
            }
            if (location.X > 660)
            {
                return new Point(0, location.Y);
            }
            if (location.Y < 0)
            {
                return new Point(location.X, 660);
            }
            if (location.Y > 660)
            {
                return new Point(location.X, 0);
            }
            throw new Exception();
        }

        private bool IsOutOfTheBoard(Button button)
        {
            if (button.Location.X < 0 || button.Location.Y < 0 || button.Location.X > 660 || button.Location.Y > 660)
            {
                return true;
            }
            return false;
        }

        private bool CheckForEating()
        {
            return TwoButtonsOverlap(this.snakeButtons.Last(), this.food);
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

