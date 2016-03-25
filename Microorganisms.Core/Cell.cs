using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Microorganisms.Core
{
    public class Cell : Microorganism
    {
        private Font font;
        private StringFormat format;


        public Point Velocity { get; set; }


        #region Initialization

        public Cell(Graphics graphic)
            : base(graphic, 20)
        {
            this.InitializeFont();
        }

        private void InitializeFont()
        {
            this.font = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            this.format = new StringFormat();
            this.format.Alignment = StringAlignment.Center;
            this.format.LineAlignment = StringAlignment.Center;
        }

        #endregion Initialization

        public void SetDirection(Point direction)
        {
            int x = 4 * direction.X / this.Mass;
            int y = 4 * direction.Y / this.Mass;

            this.Velocity = new Point(x, y);
        }

        public void Eat(List<Microorganism> microorganisms)
        {
            foreach (Microorganism microorganism in microorganisms.OfType<Nutrient>())
                this.Eat((Nutrient)microorganism); // TODO avoid Nutrient cast
        }

        public void Eat(Nutrient nutrient)
        {
            this.Mass += nutrient.Mass;
        }

        public void Eat(Cell cell)
        {
            this.Mass += cell.Mass;
        }

        public void Eat(Virus virus)
        {
            this.Divide();
        }

        public void Shoot()
        {
            //this.Mass -= 20;
        }

        public void Divide()
        {
            //this.Mass = (int)(this.Mass / 1.1);
            // TODO divide
        }
        
        #region Collisions

        public bool Collision(Microorganism microorganism)
        {
            var radius = this.Radius + microorganism.Radius;
            var deltaX = this.Center.X - microorganism.Center.X;
            var deltaY = this.Center.Y - microorganism.Center.Y;

            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }

        public bool CanEat(Microorganism microorganism)
        {
            return this.Mass * 1.25 > microorganism.Mass;
        }

        #endregion Collisions

        #region Draw

        public override void Draw(Size delta)
        {
            DrawCell(delta);
            DrawBorder(delta);
            DrawMass(delta);
        }

        private void DrawCell(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            this.graphics.FillEllipse(Brushes.Black, rectangle);
        }

        private void DrawBorder(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            rectangle.Inflate(-1, -1);
            this.graphics.FillEllipse(Brushes.Yellow, rectangle);
        }

        private void DrawMass(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            this.graphics.DrawString(this.Mass.ToString(), this.font, Brushes.Black, rectangle, format);
        }

        #endregion Draw

        public override void Dispose()
        {
            this.font.Dispose();
            this.format.Dispose();
        }
    }
}
