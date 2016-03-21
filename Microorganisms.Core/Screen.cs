using Microorganisms.Core.Controls;
using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Handle the user screen an their components.
    /// </summary>
    public class Screen
    {
        private const int padding = 55;
        private Graphics graphics;
        private Size size;
        private Size buttonSize;


        public Joystick Joistick { get; private set; }
        public Aim Aim { get; private set; }
        public Divider Divider { get; private set; }
        

        #region Initialization

        public Screen(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.size = size;

            this.buttonSize = new Size(60, 60);

            this.InitializeAim();
            this.InitializeDivider();
            this.InitializeJoystick();
        }

        private void InitializeAim()
        {
            this.Aim = new Aim(this.graphics, this.buttonSize);

            int x = this.size.Width - this.buttonSize.Width - Screen.padding;
            int y = this.size.Height - Screen.padding;
            this.Aim.Center = new Point(x, y);
        }

        private void InitializeDivider()
        {
            int x = this.size.Width - Screen.padding;
            int y = this.size.Height - this.buttonSize.Height - Screen.padding;
            this.Divider = new Divider(this.graphics, this.buttonSize, new Point(x, y));
        }

        private void InitializeJoystick()
        {
            this.Joistick = new Joystick(this.graphics);
        }

        #endregion Initialization

        public void Draw()
        {
            this.Aim.Draw();
            this.Divider.Draw();
            this.Joistick.Draw();
        }
    }
}
