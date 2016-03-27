using System;
using System.Drawing;

namespace Microorganisms.Core.Controls
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
        private Point pointer;


        public Point Direction { get; private set; }


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
            this.foot.Center = center; // TODO move in cell direction
            this.pointer = center;
            this.stick.Center = this.GetIntersection();
            this.visible = true;
        }

        public void Disable()
        {
            this.visible = false;
            this.Direction = new Point(0, 0);
        }

        public void Move(Point pointer)
        {
            this.pointer = pointer;
            this.stick.Center = this.GetIntersection();
        }

        /// <summary>
        /// Gets the intersection between the base circumference and the direction line.
        /// </summary>
        private Point GetIntersection()
        {
            int cx = this.foot.Center.X;
            int cy = this.foot.Center.Y;
            int px = this.pointer.X;
            int py = this.pointer.Y;
            int radius = this.foot.Radius;

            double lenght = Math.Sqrt((Math.Pow((px - cx), 2) + Math.Pow((py - cy), 2)));

            double x = (px - cx) / lenght;
            double y = (py - cy) / lenght;

            this.Direction = new Point((int)Math.Round(x * 100), (int)Math.Round(y * 100));
         
            if (lenght <= radius)
            {
                return this.pointer;
            }
            else
            {
                int intersectionX = (int)(this.foot.Center.X + radius * x);
                int intersectionY = (int)(this.foot.Center.Y + radius * y);

                return new Point(intersectionX, intersectionY);
            }
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
