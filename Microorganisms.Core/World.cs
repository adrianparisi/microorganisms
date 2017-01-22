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

        public World(Size world, Size client)
        {
            this.Size = world;
            this.Client = client;

            this.InitializeGraphics();    
        }

        private void InitializeGraphics()
        {
            this.backgroundBrush = new TextureBrush(Resources.Background);
            this.backgroundBrush.WrapMode = WrapMode.TileFlipXY;
        }

        #endregion Initialization
        
        #region Add

        public void AddMicroorganism(Microorganism microorganism)
        {
            while (microorganism.Collision(this))
                microorganism.Position = this.GetRandomPosition();

            this.microorganisms.Add(microorganism);
        }

        public void Add(Cell cell)
        {
            if (cell == null)
                throw new ArgumentNullException(nameof(cell));

            while (cell.Collision(this))
                cell.Position = this.GetRandomPosition();

            this.microorganisms.Add(cell);
            this.cell = cell;
        }

        private Point GetRandomPosition()
        {
            return new Point(this.random.Next(this.Size.Width), this.random.Next(this.Size.Height));
        }

        public void Add(EjectedMass mass)
        {
            if (mass == null)
                throw new ArgumentNullException(nameof(mass));

            this.microorganisms.Add(mass);
        }

        #endregion Add

        #region Remove

        public List<Microorganism> GetFood(Cell cell)
        {
            if (cell == null)
                throw new ArgumentNullException(nameof(cell));

            List<Microorganism> removed = this.microorganisms
                .Where(m => cell != m)
                .Where(m => cell.Collision(m))
                .Where(m => cell.CanEat(m))
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
                this.AddMicroorganism(new Nutrient());

            else if (microorganism.GetType() == typeof(Virus))
                this.AddMicroorganism(new Virus());

            microorganism.Dispose();
        }

        #endregion Remove

        public void Update()
        {
            foreach (Microorganism microorganism in this.microorganisms)
                microorganism.Update(this);

            this.Eat();
        }

        private void Eat()
        {
            List<Cell> cells = this.microorganisms.OfType<Cell>().ToList();

            foreach (Cell cell in cells)
            {
                List<Microorganism> microorganisms = this.GetFood(cell);

                if (microorganisms != null)
                    cell.Eat(microorganisms);
            }
        }

        #region Draw

        public void Draw(Graphics graphics)
        {
            Size delta = this.GetDeltaClient();
            this.DrawBackground(graphics, delta);
            this.DrawBorder(graphics, delta);
            this.Draw<Nutrient>(graphics, delta);
            this.Draw<EjectedMass>(graphics, delta);
            this.Draw<Cell>(graphics, delta);
            this.Draw<Virus>(graphics, delta);
        }

        /// <summary>
        /// Gets the difference between the absolute coordinates from 
        /// the world and the relative coordinates of the user client.
        /// </summary>
        private Size GetDeltaClient()
        {
            int x = this.Client.Width / 2 - this.cell.Center.X;
            int y = this.Client.Height / 2 - this.cell.Center.Y;

            return new Size(x, y);
        }

        private void DrawBackground(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(Point.Empty - this.Size + delta, this.Size.Multiply(4));
            graphics.FillRectangle(this.backgroundBrush, rectangle);
        }

        private void DrawBorder(Graphics graphics, Size delta)
        {
            var rectangle = new Rectangle(Point.Empty + delta, this.Size);
            graphics.DrawRectangle(Pens.Gray, rectangle);
        }

        private void Draw<T>(Graphics graphics, Size delta) where T : Microorganism
        {
            var microorganisms = this.microorganisms.OfType<T>();

            foreach (Microorganism microorganism in microorganisms)
                microorganism.Draw(graphics, delta);
        }

        #endregion Draw
    }
}
