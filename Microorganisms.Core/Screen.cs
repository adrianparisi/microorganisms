using Microorganisms.Core.Controls;
using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Handle the user screen an their components.
    /// </summary>
    public class Screen
    {
        private const int padding = 50;
        private Graphics graphics;
        private Size size;


        public Aim Aim { get; private set; }
        public Joystick Joistick { get; private set; }

        
        #region Initialization

        public Screen(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.size = size;

            this.InitializeAim();
            this.InitializeJoystick();
        }

        private void InitializeAim()
        {
            this.Aim = new Aim(this.graphics, new Size(60, 60));

            int x = this.size.Width - Screen.padding;
            int y = this.size.Height - Screen.padding;
            this.Aim.Center = new Point(x, y);
        }

        private void InitializeJoystick()
        {
            this.Joistick = new Joystick(this.graphics);
        }

        #endregion Initialization

        public void Draw()
        {
            this.Aim.Draw();
            this.Joistick.Draw();
        }
    }
}
