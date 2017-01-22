using Microorganisms.Core;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using SomeTools;

namespace Microorganisms.UI
{
    public partial class BoardForm : Form
    {
        private float deltaFPSTime = 0;
        private Stopwatch watch = new Stopwatch();
        Game game;


        #region Initialization

        public BoardForm()
        {
            InitializeComponent();
            this.PreventFlickering();
        }

        private void PreventFlickering()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
            this.game = new Game(this.ClientSize, this.ClientSize.Multiply(2));

            this.timer.Start();
            this.watch.Start();
        }

        #endregion Initialization

        #region Update

        private void timer_Tick(object sender, EventArgs e)
        {
            this.UpdateGame();
        }

        private void BoardForm_Paint(object sender, PaintEventArgs e)
        {
            //this.CalculateFps();
            this.game.Draw(e.Graphics);
        }

        private void UpdateGame()
        {
            this.game.Update();
            this.Invalidate();
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
                    this.Dispose();
                    this.Close();
                    break;

                default:
                    this.game.OnBoardFormKeyDown(e);
                    break;
            }
        }

        #endregion Keyboard

        #region Mouse

        private void BoardForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.game.OnMouseDown(e);
        }

        private void BoardForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.game.OnMouseUp(e);
        }

        private void BoardForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.game.OnMouseMove(e);
        }

        private void BoardForm_MouseLeave(object sender, EventArgs e)
        {
            this.game.OnMouseLeave(e);
        }

        #endregion Mouse
    }
}
