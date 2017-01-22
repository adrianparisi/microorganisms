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
    }
}
