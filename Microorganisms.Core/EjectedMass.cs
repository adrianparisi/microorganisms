using SomeTools;
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


        public Size Velocity { get; set; }


        public EjectedMass(Graphics graphics, int mass)
            : base(graphics, (int)(mass / 1.2)) { }

        public void Update()
        {
            const int aceleration = 2;

            this.Position = this.Position + Velocity;
            this.Velocity = this.Velocity.Divide(aceleration);
        }

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
