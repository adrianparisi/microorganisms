using System;
using System.Drawing;

namespace Microorganisms.Core
{
    public class Enemy : Cell
    {
        private static Random random = new Random();


        #region Initialization

        public Enemy()
        {
            this.InitializeMovemnt();
        }

        private void InitializeMovemnt()
        {
            const int maximum = 10;
            Point direction = Point.Empty;

            while (direction == Point.Empty)
            {
                int x = Enemy.random.Next(-1 * maximum, maximum);
                int y = Enemy.random.Next(-1 * maximum, maximum);
                direction = new Point(x, y);
            }

            this.SetDirection(direction);
        }

        #endregion Initialization

        public override void Update(World world)
        {
            Point position = this.Position + new Size(this.Velocity);

            if (!this.Collision(position, world))
                this.Position = position;

            else this.Bounce(world);
        }

        private void Bounce(World world)
        {
            Point position = this.Position + new Size(this.Velocity);
            Point horizontal = new Point(position.X, this.Position.Y);

            if (!this.Collision(horizontal, world))
            {
                this.Velocity = new Point(this.Velocity.X, -1 * this.Velocity.Y);
                this.Position = this.Position + new Size(this.Velocity);

                return;
            }

            Point vertical = new Point(this.Position.X, position.Y);

            if (!this.Collision(vertical, world))
            {
                this.Velocity = new Point(-1 * this.Velocity.X, this.Velocity.Y);
                this.Position = this.Position + new Size(this.Velocity);

                return;
            }
        }
    }
}
