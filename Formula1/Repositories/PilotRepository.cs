using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<Pilot>
    {
        private List<Pilot> models;

        public PilotRepository()
        {
            this.models = new List<Pilot>();
        }

        public IReadOnlyCollection<Pilot> Models => this.models.AsReadOnly();

        public void Add(Pilot model)
        {
            this.models.Add(model);
        }

        public Pilot FindByName(string name)
            => this.models.Find(p => p.FullName == name);

        public bool Remove(Pilot model)
            => this.models.Remove(model);
    }
}
