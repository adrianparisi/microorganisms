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


        public Virus()
            : base(140) { }

        public override void Draw(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(this.Position + delta, this.Size);
            graphics.DrawImage(Virus.image, rectangle);
        }

        public override void Dispose() { }
    }
}
