using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Control to handle the movement with the mouse.
    /// </summary>
    public class Joystick
    {
        private Graphics graphics;
        private bool visible;
        private JoystickComponent foot;
        private JoystickComponent stick;
        private Point direction;


        #region Initialization

        public Joystick(Graphics graphics)
        {
            this.graphics = graphics;
            this.InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            this.foot = new JoystickComponent(this.graphics, new Size(60, 60));
            this.stick = new JoystickComponent(this.graphics, new Size(30, 30));
            this.visible = false;
        }

        #endregion Initialization
        
        #region Behaviour

        public void Enable(Point center)
        {
            this.foot.Center = center;
            this.direction = center;
            this.stick.Center = this.GetIntersection();
            this.visible = true;
        }

        public void Disable()
        {
            this.visible = false;
        }

        public void Move(Point direction)
        {
            this.direction = direction;
            this.stick.Center = this.GetIntersection();
        }

        /// <summary>
        /// Gets the intersection between the base circumference and the direction line.
        /// </summary>
        private Point GetIntersection()
        {
            //var u = this.foot.Center.X;
            //var v = this.foot.Center.Y;
            //var w = this.direction.X;
            //var z = this.direction.Y;
            //var r = 30;
            //var m = (v - z) / (u - w);

            return this.direction;
        }

        #endregion Behaviour

        public void Draw()
        {
            if (this.visible)
            {
                this.foot.Draw();
                this.stick.Draw();
            }
        }
    }
}
