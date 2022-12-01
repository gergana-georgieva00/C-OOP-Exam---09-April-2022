using Formula1.Models;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<Race>
    {
        private List<Race> models;

        public RaceRepository()
        {
            this.models = new List<Race>();
        }

        public IReadOnlyCollection<Race> Models => this.models.AsReadOnly();

        public void Add(Race model)
        {
            this.models.Add(model);
        }

        public Race FindByName(string name)
            => this.models.Find(r => r.RaceName == name);

        public bool Remove(Race model)
            => this.models.Remove(model);
    }
}
