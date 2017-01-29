using System.Drawing;

namespace Microorganisms.Core.Screens
{
    abstract class Screen
    {
        public Size Size { get; protected set; }

        public int Height
        {
            get { return this.Size.Height; }
        }

        public int Width
        {
            get { return this.Size.Width; }
        }
    }
}
