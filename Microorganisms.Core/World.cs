using Microorganisms.Core.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microorganisms.Core
{
    public class World
    {
        private Random random = new Random();
        private Graphics graphics;
        private List<Microorganism> microorganisms = new List<Microorganism>();


        public Size Size { get; private set; }

        public Point Center
        {
            get { return new Point(this.Size.Width / 2, this.Size.Height / 2); }
        }


        #region Initialization

        public World(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.Size = size;

            this.InitializeNutrients();
            this.InitializeVirus();
        }

        private void InitializeNutrients()
        {
            const int nutrientsCount = 100;

            for (int i = 0; i < nutrientsCount; i++)
                this.AddNutrient();
        }

        private void InitializeVirus()
        {
            const int virusCount = 15;

            for (int i = 0; i < virusCount; i++)
                this.AddVirus();
        }

        #endregion Initialization

        private void AddNutrient()
        {
            Nutrient nutrient = new Nutrient(this.graphics);
            nutrient.Position = this.GetRandomPosition();
            this.microorganisms.Add(nutrient);
        }

        private void AddVirus()
        {
            Virus virus = new Virus(this.graphics);
            virus.Position = this.GetRandomPosition();
            this.microorganisms.Add(virus);
        }

        private Point GetRandomPosition()
        {
            Point? position = null;

            while (!position.HasValue) // TODO validate collisions
            {
                position = new Point(random.Next(this.Size.Width), random.Next(this.Size.Height));
            }

            return position.Value;
        }

        public void Add(Cell cell)
        {
            cell.Position = this.Center;
            this.microorganisms.Add(cell);
        }

        public List<Microorganism> GetFood(Cell cell)
        {
            List<Microorganism> removed = this.microorganisms
                .Where(m => cell.CanEat(m))
                .Where(m => cell.Collision(m))
                .Where(m => cell != m)
                .ToList();

            foreach (Microorganism microorganism in removed)
                this.Remove(microorganism);

            return removed;
        }

        private void Remove(Microorganism microorganism)
        {
            this.microorganisms.Remove(microorganism);
            this.Replace(microorganism);
        }

        private void Replace(Microorganism microorganism)
        {
            Type type = microorganism.GetType();

            if (microorganism.GetType() == typeof(Nutrient))
                this.AddNutrient();

            else if (microorganism.GetType() == typeof(Virus))
                this.AddVirus();
        }

        #region Draw

        public void Draw()
        {
            this.graphics.Clear(Color.White);
            this.DrawBackground();
            this.DrawBorder();
            this.Draw<Nutrient>();
            this.Draw<Cell>();
            this.Draw<Virus>();
        }

        private void DrawBackground()
        {
            TextureBrush brush = new TextureBrush(Resources.Background);
            Pen blackPen = new Pen(Color.Black);
            brush.WrapMode = WrapMode.TileFlipXY;
            this.graphics.FillRectangle(brush, new Rectangle(0, 0, this.Size.Width, this.Size.Height));
        }

        private void DrawBorder()
        {
            this.graphics.DrawLine(Pens.LightGray, 0, 0, this.Size.Width, 0);
            this.graphics.DrawLine(Pens.LightGray, 0, 0, 0, this.Size.Height);
            this.graphics.DrawLine(Pens.LightGray, 0, this.Size.Height, this.Size.Width, this.Size.Height);
            this.graphics.DrawLine(Pens.LightGray, this.Size.Width, 0, this.Size.Width, this.Size.Height);
        }

        private void Draw<T>() where T : Microorganism
        {
            var microorganisms = this.microorganisms.OfType<T>();

            foreach (Microorganism microorganism in microorganisms)
                microorganism.Draw();
        }

        #endregion Draw
    }
}
