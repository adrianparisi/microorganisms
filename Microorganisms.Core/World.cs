using System;
using System.Collections.Generic;
using System.Drawing;
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

            while (!position.HasValue) // TODO validate collision with cells
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
                .Where(m => cell.Mass > m.Mass)
                .Where(m => cell.Collision(m))
                .Where(m => m != cell)
                .ToList();

            foreach (Microorganism microorganism in removed)
                this.Remove(microorganism);

            return removed;
        }

        private void Remove(Microorganism microorganism)
        {
            this.microorganisms.Remove(microorganism);

            if (microorganism.GetType() == typeof(Nutrient))
                this.AddNutrient();
        }

        #region Draw

        public void Draw()
        {
            this.graphics.Clear(Color.White);
            this.Draw<Nutrient>();
            this.Draw<Cell>();
            this.Draw<Virus>();
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
