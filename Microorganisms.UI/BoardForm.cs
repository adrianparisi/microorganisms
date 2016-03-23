using Microorganisms.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

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
        private Core.Screen screen;
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
            this.screen = new Core.Screen(this.graphics, this.ClientSize);
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
            this.screen.Draw();
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

        #region Keyboard

        private void BoardForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Oem5: // to the left of the 1 key
                    this.lblFps.Visible = !this.lblFps.Visible;
                    this.label1.Visible = !this.label1.Visible;
                    break;
                    
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        #endregion Keyboard

        #region Mouse

        private void BoardForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.screen.Joistick.Enable(e.Location);
        }

        private void BoardForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.screen.Joistick.Disable();
        }

        private void BoardForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.screen.Joistick.Move(e.Location);
                this.cell.SetDirection(this.screen.Joistick.Direction);
            }
        }

        #endregion Mouse
    }
}
