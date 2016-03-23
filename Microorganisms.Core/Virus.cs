﻿using Microorganisms.Core.Properties;
using System.Drawing;

namespace Microorganisms.Core
{
    /// <summary>
    /// Hurt the cells when collide.
    /// </summary>
    /// <seealso cref="Microorganisms.Core.Microorganism" />
    public class Virus : Microorganism
    {
        private static Bitmap image = Resources.Virus64x64;


        public Virus(Graphics graphics)
            : base(graphics, 140)
        {
            this.Width = 45;
            this.Height = 45;
        }


        public override void Draw(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Radius * 2, this.Radius * 2));
            this.graphics.DrawImage(Virus.image, rectangle);
        }
    }
}
