using SomeTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Microorganisms.Core
{
    public class Cell : Microorganism
    {
        private Font font;
        private StringFormat format;


        #region Initialization

        public Cell()
            : base(20)
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
        
        #region Eat

        public void Eat(List<Microorganism> microorganisms)
        {
            var virus = microorganisms.OfType<Virus>();

            foreach (Virus v in virus)
                this.Eat(v);

            var others = microorganisms.Except(virus);

            foreach (Microorganism microorganism in others)
                this.Eat(microorganism);
        }

        public void Eat(Microorganism microorganism)
        {
            this.Mass += microorganism.Mass;
        }

        public void Eat(Virus virus)
        {
            this.Divide();
        }

        #endregion Eat
        
        #region Eject
        
        public EjectedMass EjectMass()
        {
            if (this.Mass > 35)
            {
                const int ejected = 20;
                this.Mass -= ejected;
                EjectedMass mass = new EjectedMass(ejected);
                mass.Velocity = new Point(new Size(this.Velocity).Multiply(14));
                mass.Position = this.GetIntersection(this.Center, this.Velocity) + new Size(mass.Velocity);

                return mass;
            }

            return null;
        }

        private Point GetIntersection(Point center, Point pointer)
        {
            int cx = center.X;
            int cy = center.Y;
            int px = pointer.X;
            int py = pointer.Y;

            int radius = this.Size.Width / 2;
            double lenght = Math.Sqrt((Math.Pow((px - cx), 2) + Math.Pow((py - cy), 2)));

            int x = (int)(cx + radius * (px - cx) / lenght);
            int y = (int)(cy + radius * (py - cy) / lenght);

            return new Point(x, y);
        }

        #endregion Eject

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

        public override void Draw(Graphics graphics, Size delta)
        {
            DrawCell(graphics, delta);
            DrawBorder(graphics, delta);
            DrawMass(graphics, delta);
        }

        private void DrawCell(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            graphics.FillEllipse(Brushes.Black, rectangle);
        }

        private void DrawBorder(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            rectangle.Inflate(-1, -1);
            graphics.FillEllipse(Brushes.Yellow, rectangle);
        }

        private void DrawMass(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            graphics.DrawString(this.Mass.ToString(), this.font, Brushes.Black, rectangle, format);
        }

        #endregion Draw

        public override void Dispose()
        {
            this.font.Dispose();
            this.format.Dispose();
        }
    }
}
