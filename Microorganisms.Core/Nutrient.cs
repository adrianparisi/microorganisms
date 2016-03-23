using System;
using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// The Cell's food.
    /// </summary>
    /// <seealso cref="Microorganisms.Core.Microorganism" />
    public class Nutrient : Microorganism
    {
        private static Random random = new Random();
        private Brush brush;


        public Color Color { get; private set; }

        public new int Radius
        {
            get { return 4; }
        }


        #region Initialization

        public Nutrient(Graphics graphics)
            : base(graphics, 1)
        {
            this.Color = GetRandomColor();
            this.brush = new SolidBrush(this.Color);
        }

        private static Color GetRandomColor()
        {
            return Color.FromArgb(Nutrient.random.Next(255), Nutrient.random.Next(255), Nutrient.random.Next(255));
        }

        #endregion Initialization

        public override void Draw(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Radius * 2, this.Radius * 2));
            this.graphics.FillEllipse(this.brush, rectangle);
        }
    }
}
