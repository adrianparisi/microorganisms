using SomeTools;
using System;
using System.Drawing;

namespace Microorganisms.Core
{
    public abstract class Microorganism : IDisposable
    {
        private int mass;
        protected Graphics graphics;


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


        public Microorganism(Graphics graphics, int mass)
        {
            this.graphics = graphics;
            this.Mass = mass;
        }

        public void Update()
        {
            this.Velocity = this.Velocity + new Size(this.Aceleration);
            this.Position = this.Position + new Size(this.Velocity);

            // TODO check world collision
        }

        public bool Collision(World world)
        {
            return this.Position.X <= 0 || this.Position.X + this.Size.Width >= world.Size.Width ||
                this.Position.Y <= 0 || this.Position.Y + this.Size.Height >= world.Size.Height;
        }

        public abstract void Draw(Size delta);

        public abstract void Dispose();
    }
}
