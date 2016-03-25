using Microorganisms.Core.Properties;
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
            : base(graphics, 140) { }

        public override void Draw(Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            this.graphics.DrawImage(Virus.image, rectangle);
        }

        public override void Dispose() { }
    }
}
