using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microorganisms.Core
{
    public class Cell : Microorganism
    {
        public Cell(Graphics graphic)
            : base(graphic, 20)
        {
            this.Height = 30;
            this.Width = 30;
        }

        public void Eat(List<Microorganism> microorganisms)
        {
            foreach (Microorganism microorganism in microorganisms.OfType<Nutrient>())
                this.Eat((Nutrient)microorganism); // TODO avoid Nutrient cast
        }

        public void Eat(Nutrient nutrient)
        {
            this.Mass += nutrient.Mass;
            this.Grow();
        }

        public void Eat(Cell cell)
        {
            this.Mass += cell.Mass;
            this.Grow();
        }

        private void Grow()
        {
            int half = (int)Math.Round(Math.Sqrt(this.Mass / Math.PI), 0);
            this.Width += half;
            this.Height += half;
            // TODO slow down
        }

        public void Eat(Virus virus)
        {
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

        public bool Collision(Size world)
        {
            return this.Position.X <= 0 || this.Position.X + this.Radius >= world.Width ||
                this.Position.Y <= 0 || this.Position.Y + this.Radius >= world.Height;
        }

        #endregion Collisions

        #region Draw

        public override void Draw()
        {
            DrawCell();
            DrawBorder();
            DrawMass();
        }

        private void DrawCell()
        {
            Rectangle rectangle = new Rectangle(this.Position.X, this.Position.Y, this.Width, this.Height);
            this.graphics.FillEllipse(Brushes.Black, rectangle);
        }

        private void DrawBorder()
        {
            Rectangle rectangle = new Rectangle(this.Position.X, this.Position.Y, this.Width, this.Height);
            rectangle.Inflate(-1, -1);
            this.graphics.FillEllipse(Brushes.Yellow, rectangle);
        }

        private void DrawMass()
        {
            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Rectangle rectangle = new Rectangle(this.Position.X, this.Position.Y, this.Width, this.Height);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            this.graphics.DrawString(this.Mass.ToString(), font, Brushes.Black, rectangle, format);

            font.Dispose();
        }

        #endregion Draw
    }
}
