using Microorganisms.Core.Properties;
using SomeTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Microorganisms.Core
{
    /// <summary>
    /// The place where live all the microorganisms.
    /// </summary>
    public class World
    {
        private Random random = new Random();
        private Graphics graphics;
        private TextureBrush backgroundBrush;
        private List<Microorganism> microorganisms = new List<Microorganism>();
        private Cell cell;


        public Size Size { get; private set; }
        public Size Client { get; private set; }

        public Point Center
        {
            get { return new Point(this.Size.Divide(2)); }
        }


        #region Initialization

        public World(Graphics graphics, Size world, Size client)
        {
            this.Size = world;
            this.Client = client;

            this.InitializeGraphics(graphics);
            this.InitializeNutrients();
            this.InitializeVirus();            
        }

        private void InitializeGraphics(Graphics graphics)
        {
            this.graphics = graphics;
            this.backgroundBrush = new TextureBrush(Resources.Background);
            this.backgroundBrush.WrapMode = WrapMode.TileFlipXY;
        }

        private void InitializeNutrients()
        {
            const int nutrientsCount = 180;

            for (int i = 0; i < nutrientsCount; i++)
                this.AddNutrient();
        }

        private void InitializeVirus()
        {
            const int virusCount = 10;

            for (int i = 0; i < virusCount; i++)
                this.AddVirus();
        }

        #endregion Initialization
        
        #region Add

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
            this.cell = cell;
        }

        #endregion Add

        #region Remove

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

        #endregion Remove

        #region Draw

        public void Draw()
        {
            Size delta = this.GetDeltaClient();
            this.DrawBackground(delta);
            this.DrawBorder(delta);
            this.Draw<Nutrient>(delta);
            this.Draw<Cell>(delta);
            this.Draw<Virus>(delta);
        }

        /// <summary>
        /// Gets the difference between the absolute coordinates from 
        /// the world and the relative coordinates of the user client.
        /// </summary>
        private Size GetDeltaClient()
        {
            int x = (this.cell.Center.X - this.Client.Width / 2) * -1;
            int y = (this.cell.Center.Y - this.Client.Height / 2) * -1;

            return new Size(x, y);
        }

        private void DrawBackground(Size delta)
        {
            var rectangle = new Rectangle(new Point(0, 0), this.Size + this.Size);
            this.graphics.FillRectangle(this.backgroundBrush, rectangle);
        }

        private void DrawBorder(Size delta)
        {
            var rectangle = new Rectangle(new Point(0, 0) + delta, this.Size);
            this.graphics.DrawRectangle(Pens.Gray, rectangle);
        }

        private void Draw<T>(Size delta) where T : Microorganism
        {
            var microorganisms = this.microorganisms.OfType<T>();

            foreach (Microorganism microorganism in microorganisms)
                microorganism.Draw(delta);
        }

        #endregion Draw
    }
}
