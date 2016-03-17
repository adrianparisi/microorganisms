using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microorganisms.Core
{
    /// <summary>
    /// When they collide hurt cells.
    /// </summary>
    /// <seealso cref="Microorganisms.Core.Microorganism" />
    public class Virus : Microorganism
    {
        public Virus(Graphics graphics)
            : base(graphics, 40)
        {
            this.Width = 45;
            this.Height = 45;
        }


        public override void Draw(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, new Size(this.Radius * 2, this.Radius * 2));
            this.graphics.FillEllipse(Brushes.DarkOliveGreen, rectangle);
        }
    }
}
