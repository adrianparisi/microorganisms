using System.Drawing;

namespace Microorganisms.Core.Screens
{
    class FilterSreen : Screen
    {
        private static SolidBrush filter;


        public FilterSreen(Size size, Color color)
        {
            this.Size = size;
            FilterSreen.filter = new SolidBrush(color);
        }

        public void Draw(Graphics graphics)
        {
            var rectangle = new Rectangle(Point.Empty, this.Size);
            graphics.FillRectangle(FilterSreen.filter, rectangle);
        }
    }
}
