using Microorganisms.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Microorganisms.UI
{
    public partial class BoardForm : Form
    {
        private float deltaFPSTime = 0;
        private Stopwatch watch = new Stopwatch();
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
        }

        private void BoardForm_Paint(object sender, PaintEventArgs e)
        {
            //this.UpdateGame();
            this.CalculateFps();
        }

        private void UpdateGame()
        {
            this.world.Update();
            this.world.Draw();
            this.screen.Draw();
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
                case Keys.W:
                    EjectedMass mass = this.cell.EjectMass();

                    if (mass != null)
                        this.world.Add(mass);
                    break;

                case Keys.Space:
                    this.cell.Divide();
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

        #endregion Keyboard

        #region Mouse

        private void BoardForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.screen.Joistick.Enable(e.Location);
        }

        private void BoardForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.screen.Joistick.Move(e.Location);
                this.cell.SetDirection(this.screen.Joistick.Direction);
            }
        }

        private void BoardForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.screen.Joistick.Disable();
        }

        private void BoardForm_MouseLeave(object sender, EventArgs e)
        {
            this.screen.Joistick.Disable();
        }

        #endregion Mouse
    }
}
