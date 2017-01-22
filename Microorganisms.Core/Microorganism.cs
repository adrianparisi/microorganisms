using SomeTools;
using System;
using System.Drawing;

namespace Microorganisms.Core
{
    public abstract class Microorganism : IDisposable
    {
        private int mass;


        public int Mass
        {
            get { return this.mass; }

            protected set
            {
                this.mass = value;
                int side = (int)Math.Round(Math.Sqrt(value / Math.PI) * 2 * 5);
                this.Size = new Size(side, side);
            }
        }

        public Size Size { get; private set; }
        public int Radius
        {
            get { return this.Size.Width / 2; }
        }

        /// <summary>
        /// Gets or sets the upper left corner.
        /// </summary>
        public Point Position { get; set; }

        public Point Center
        {
            get { return this.Position + this.Size.Divide(2); }
        }

        public Point Velocity { get; set; }
        public Point Aceleration { get; set; }


        public Microorganism(int mass)
        {
            this.Mass = mass;
        }

        public void Update(World world)
        {
            Point velocity = this.Velocity + new Size(this.Aceleration);
            Point position = this.Position + new Size(this.Velocity);

            if (!this.Collision(position, world))
            {
                this.Velocity = velocity;
                this.Position = position;

                return;
            }

            Point horizontal = new Point(position.X, this.Position.Y);

            if (!this.Collision(horizontal, world))
            {
                this.Velocity = velocity;
                this.Position = horizontal;

                return;
            }

            Point vertical = new Point(this.Position.X, position.Y);

            if (!this.Collision(vertical, world))
            {
                this.Velocity = velocity;
                this.Position = vertical;

                return;
            }
        }

        public bool Collision(World world)
        {
            return this.Collision(this.Position, world);
        }

        private bool Collision(Point position, World world)
        {
            return position.X <= 0 || position.X + this.Size.Width >= world.Size.Width ||
                position.Y <= 0 || position.Y + this.Size.Height >= world.Size.Height;
        }

        public abstract void Draw(Graphics graphics, Size delta);

        public abstract void Dispose();
    }
}
