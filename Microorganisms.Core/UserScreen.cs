using Microorganisms.Core.Controls;
using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Handle the user screen an their components.
    /// </summary>
    public class UserScreen
    {
        private const int padding = 55;
        private Size size;
        private Size buttonSize;


        public Joystick Joistick { get; private set; }
        public Aim Aim { get; private set; }
        public Divider Divider { get; private set; }
        

        #region Initialization

        public UserScreen(Size size)
        {
            this.size = size;

            this.buttonSize = new Size(60, 60);

            this.InitializeAim();
            this.InitializeDivider();
            this.InitializeJoystick();
        }

        private void InitializeAim()
        {
            this.Aim = new Aim(this.buttonSize);

            int x = this.size.Width - this.buttonSize.Width - UserScreen.padding;
            int y = this.size.Height - UserScreen.padding;
            this.Aim.Center = new Point(x, y);
        }

        private void InitializeDivider()
        {
            int x = this.size.Width - UserScreen.padding;
            int y = this.size.Height - this.buttonSize.Height - UserScreen.padding;
            this.Divider = new Divider(this.buttonSize, new Point(x, y));
        }

        private void InitializeJoystick()
        {
            this.Joistick = new Joystick();
        }

        #endregion Initialization

        public void Draw(Graphics graphics)
        {
            this.Aim.Draw(graphics);
            this.Divider.Draw(graphics);
            this.Joistick.Draw(graphics);
        }
    }
}
