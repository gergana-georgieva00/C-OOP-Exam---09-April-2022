using Formula1.Models;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<FormulaOneCar>
    {
        private List<FormulaOneCar> models;

        public FormulaOneCarRepository()
        {
            this.models = new List<FormulaOneCar>();
        }

        public IReadOnlyCollection<FormulaOneCar> Models => this.models;

        public void Add(FormulaOneCar model)
        {
            this.models.Add(model);
        }

        public FormulaOneCar FindByName(string name)
            => this.models.Find(c => c.GetType().Name == name);

        public bool Remove(FormulaOneCar model)
            => this.models.Remove(model);
    }
}
