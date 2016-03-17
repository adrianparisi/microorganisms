using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microorganisms.Core
{
    public abstract class Microorganism
    {
        protected Graphics graphics;


        public Point Position { get; set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int Mass { get; protected set; }

        public int Radius
        {
            get { return this.Width / 2; }
        }

        public Point Center
        {
            get
            {
                int x = this.Position.X + this.Width / 2;
                int y = this.Position.Y + this.Height / 2;

                return new Point(x, y);
            }
        }

        public Microorganism(Graphics graphics, int mass)
        {
            this.graphics = graphics;
            this.Mass = mass;
        }

        public abstract void Draw();
    }
}
