﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Microorganisms.Core
{
    public class Cell : Microorganism
    {
        public Point Velocity { get; set; }


        public Cell(Graphics graphic)
            : base(graphic, 20) { }

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

        public bool Collision(Size world)
        {
            return this.Position.X <= 0 || this.Position.X + this.Radius >= world.Width ||
                this.Position.Y <= 0 || this.Position.Y + this.Radius >= world.Height;
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
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Width, this.Height));
            this.graphics.FillEllipse(Brushes.Black, rectangle);
        }

        private void DrawBorder(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Width, this.Height));
            rectangle.Inflate(-1, -1);
            this.graphics.FillEllipse(Brushes.Yellow, rectangle);
        }

        private void DrawMass(Size delta)
        {
            Font font = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Width, this.Height));
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            this.graphics.DrawString(this.Mass.ToString(), font, Brushes.Black, rectangle, format);

            font.Dispose();
        }

        #endregion Draw
    }
}
