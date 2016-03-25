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

        public Point Position { get; set; }

        public Point Center
        {
            get { return this.Position + this.Size.Divide(2); }
        }


        public Microorganism(Graphics graphics, int mass)
        {
            this.graphics = graphics;
            this.Mass = mass;
        }

        public bool Collision(Size world)
        {
            return this.Position.X <= 0 || this.Position.X + this.Size.Width >= world.Width ||
                this.Position.Y <= 0 || this.Position.Y + this.Size.Height >= world.Height;
        }

        public abstract void Draw(Size delta);

        public abstract void Dispose();
    }
}
