using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Mass ejected by cells.
    /// </summary>
    /// <seealso cref="Microorganisms.Core.Microorganism" />
    public class EjectedMass : Microorganism
    {
        private Pen borderPen;
        private Brush backgroundBrush;


        #region Initialization

        public EjectedMass(int mass)
            : base(EjectedMass.ReduceMass(mass))
        {
            this.InitializeGraphics();
        }

        /// <summary>
        /// Reduces the quantity of mass that the cells eject.
        /// </summary>
        /// <param name="mass">The mass ejected by the cell.</param>
        private static int ReduceMass(int mass)
        {
            return (int)(mass / 1.2);
        }

        private void InitializeGraphics()
        {
            this.borderPen = new Pen(Color.SaddleBrown, 4);
            this.backgroundBrush = new SolidBrush(Color.Sienna);
        }

        #endregion Initialization

        #region Draw

        public override void Draw(Graphics graphics, Size delta)
        {
            this.DrawBackground(graphics, delta);
            this.DrawBorder(graphics, delta);
        }

        private void DrawBackground(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            graphics.FillEllipse(this.backgroundBrush, rectangle);
        }

        private void DrawBorder(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            rectangle.Inflate(-1, -1);
            graphics.DrawEllipse(this.borderPen, rectangle);
        }

        #endregion Draw

        public override void Dispose()
        {
            this.backgroundBrush.Dispose();
            this.borderPen.Dispose();
        }
    }
}
