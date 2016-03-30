using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Mass ejected by cells.
    /// </summary>
    /// <seealso cref="Microorganisms.Core.Microorganism" />
    public class EjectedMass : Microorganism
    {
        private static Brush borderBrush = new SolidBrush(Color.Sienna);
        private static Brush backgroundBrush = new SolidBrush(Color.SaddleBrown);


        #region Initialization

        public EjectedMass(Graphics graphics, int mass)
            : base(graphics, EjectedMass.ReduceMass(mass)) { }

        /// <summary>
        /// Reduces the quantity of mass that the cells eject.
        /// </summary>
        /// <param name="mass">The mass ejected by the cell.</param>
        private static int ReduceMass(int mass)
        {
            return (int)(mass / 1.2);
        }

        #endregion Initialization

        #region Draw

        public override void Draw(Size delta)
        {
            this.DrawBackground(delta);
            this.DrawBorder(delta);
        }

        private void DrawBackground(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            this.graphics.FillEllipse(EjectedMass.backgroundBrush, rectangle);
        }

        private void DrawBorder(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            rectangle.Inflate(-1, -1);
            this.graphics.FillEllipse(EjectedMass.borderBrush, rectangle);
        }

        #endregion Draw

        public override void Dispose() { }
    }
}
