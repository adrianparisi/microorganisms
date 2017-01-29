using Microorganisms.Core.Screens;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microorganisms.Core
{
    public class Game
    {
        private bool pause;
        private World world;
        private UserScreen screen;
        private Cell cell;
        private PauseScreen pauseScreen;


        #region Initialization

        public Game(Size clientSize, Size worldSize)
        {
            this.world = new World(worldSize, clientSize);
            this.screen = new UserScreen(clientSize);
            this.cell = new Cell();
            this.world.Add(this.cell);
            this.InitializeNutrients();
            this.InitializeVirus();
            this.InitializeEnemies();
            this.pauseScreen = new PauseScreen(clientSize);
        }

        private void InitializeNutrients()
        {
            const int nutrientsCount = 180;

            for (int i = 0; i < nutrientsCount; i++)
                this.world.AddMicroorganism(new Nutrient());
        }

        private void InitializeVirus()
        {
            const int virusCount = 10;

            for (int i = 0; i < virusCount; i++)
                this.world.AddMicroorganism(new Virus());
        }

        private void InitializeEnemies()
        {
            const int enemiesCount = 10;

            for (int i = 0; i < enemiesCount; i++)
                this.world.AddMicroorganism(new Enemy());
        }

        #endregion Initialization

        #region Keyboard

        public void OnBoardFormKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Pause:
                    this.Pause();
                    break;

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

        private void Pause()
        {
            this.pause = !this.pause;
            this.pauseScreen.Enable = this.pause;
        }

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
            this.pauseScreen.Draw(graphics);
        }
    }
}
