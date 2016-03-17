using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microorganisms.Core;
using System.Diagnostics;

namespace Microorganisms.UI
{
    public partial class BoardForm : Form
    {
        private float deltaFPSTime = 0;
        private Stopwatch watch = new Stopwatch();

        private int minVelocity = 5;
        private int maxVelocity = 15;

        private Graphics graphics;
        private World world;
        private Cell cell;


        #region Initialization

        public BoardForm()
        {
            InitializeComponent();

            this.graphics = this.CreateGraphics();
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
            this.cell = new Cell(this.graphics);
            var size = new Size(this.ClientSize.Width * 2, this.ClientSize.Height * 2);
            this.world = new World(this.graphics, size, this.ClientSize);
            this.world.Add(this.cell);

            this.timer.Start();
            this.watch.Start();
        }

        #endregion Initialization

        #region Update

        private void timer_Tick(object sender, EventArgs e)
        {
            this.UpdateGame();
            this.CalculateFps();
        }

        private void UpdateGame()
        {
            this.world.Draw();
            this.MoveCell();
            this.Eat();
        }

        private void CalculateFps()
        {
            float elapsed = (float)watch.ElapsedMilliseconds / 1000;
            float fps = 1 / elapsed;
            this.deltaFPSTime += elapsed;

            if (this.deltaFPSTime > 1)
            {
                this.lblFps.Text = fps.ToString("N2");
                this.deltaFPSTime -= 1;
            }
        }

        private void MoveCell()
        {
            Point position = new Point();
            position.X = MoveX(this.cell);
            position.Y = MoveY(this.cell);

            this.cell.Position = position;
        }

        private int MoveX(Cell cell)
        {
            if (cell.Velocity.X != 0)
            {
                int x = cell.Position.X + cell.Velocity.X;

                if (0 < x && x + cell.Width < this.world.Size.Width)
                    return x;
            }

            return cell.Position.X;
        }

        private int MoveY(Cell cell)
        {
            if (cell.Velocity.Y != 0)
            {
                int y = cell.Position.Y + cell.Velocity.Y;

                if (0 < y && y + cell.Height < this.world.Size.Height)
                    return y;
            }

            return cell.Position.Y;
        }

        private void Eat()
        {
            List<Microorganism> microorganisms = this.world.GetFood(this.cell);

            if (microorganisms != null)
                this.cell.Eat(microorganisms);
        }

        #endregion Update

        #region Keys

        private void BoardForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    this.cell.Velocity = new Point(this.cell.Velocity.X, this.maxVelocity * -1);
                    break;

                case Keys.Down:
                case Keys.S:
                    this.cell.Velocity = new Point(this.cell.Velocity.X, this.maxVelocity);
                    break;

                case Keys.Right:
                case Keys.D:
                    this.cell.Velocity = new Point(this.maxVelocity, this.cell.Velocity.Y);
                    break;

                case Keys.Left:
                case Keys.A:
                    this.cell.Velocity = new Point(this.maxVelocity * -1, this.cell.Velocity.Y);
                    break;

                case Keys.Oem5: // to the left of the 1 key
                    this.lblFps.Visible = !this.lblFps.Visible;
                    this.label1.Visible = !this.label1.Visible;
                    break;
                    
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void BoardForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    this.cell.Velocity = new Point(this.cell.Velocity.X, this.minVelocity * -1);
                    break;

                case Keys.Down:
                case Keys.S:
                    this.cell.Velocity = new Point(this.cell.Velocity.X, this.minVelocity);
                    break;

                case Keys.Right:
                case Keys.D:
                    this.cell.Velocity = new Point(this.minVelocity, this.cell.Velocity.Y);
                    break;

                case Keys.Left:
                case Keys.A:
                    this.cell.Velocity = new Point(this.minVelocity * -1, this.cell.Velocity.Y);
                    break;
            }
        }

        #endregion Keys
    }
}
