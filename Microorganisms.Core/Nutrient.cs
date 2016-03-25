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
        private Size size;
        private Brush brush;


        public Color Color { get; private set; }

        public new Size Size
        {
            get { return this.size; }
        }


        #region Initialization

        public Nutrient(Graphics graphics)
            : base(graphics, 1)
        {
            this.Color = GetRandomColor();
            this.size = new Size(8, 8);
            this.brush = new SolidBrush(this.Color);
        }

        private static Color GetRandomColor()
        {
            return Color.FromArgb(Nutrient.random.Next(255), Nutrient.random.Next(255), Nutrient.random.Next(255));
        }

        #endregion Initialization

        public override void Draw(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            this.graphics.FillEllipse(this.brush, rectangle);
        }

        public override void Dispose()
        {
            this.brush.Dispose();
        }
    }
}
