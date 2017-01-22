using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microorganisms.Core
{
    public class Game
    {
        private World world;
        private UserScreen screen;
        private Cell cell;


        public Game(Size clientSize, Size worldSize)
        {
            this.cell = new Cell();
            this.world = new World(worldSize, clientSize);
            this.screen = new UserScreen(clientSize);
            this.world.Add(this.cell);
        }

        #region Keyboard

        public void OnBoardFormKeyDown(KeyEventArgs e)
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
            }
        }

        #endregion Keyboard

        #region Mouse

        public void OnMouseDown(MouseEventArgs e)
        {
            this.screen.Joistick.Enable(e.Location);
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            this.screen.Joistick.Disable();
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.screen.Joistick.Move(e.Location);
                this.cell.SetDirection(this.screen.Joistick.Direction);
            }
        }

        public void OnMouseLeave(EventArgs e)
        {
            this.screen.Joistick.Disable();
        }

        #endregion Mouse

        public void Update()
        {
            this.world.Update();
        }

        public void Draw(Graphics graphics)
        {
            this.world.Draw(graphics);
            this.screen.Draw(graphics);
        }
    }
}
