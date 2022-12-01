using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;

        public Race(string raceName, int numberOfLaps)
        {
            this.RaceName = raceName;
            this.NumberOfLaps = numberOfLaps;
            this.TookPlace = false;
        }

        public string RaceName 
        {
            get => this.raceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException($"Invalid race name: {value}.");
                this.raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get => this.numberOfLaps;
            private set
            {
                if (value < 1)
                    throw new ArgumentException($"Invalid lap numbers: {value}.");
                this.numberOfLaps = value;
            }
        }

        public bool TookPlace  { get; set; }

        public ICollection<IPilot> Pilots { get; private set; }

        public void AddPilot(IPilot pilot)
        {
            this.Pilots.Add(pilot);
        }

        public string RaceInfo()
            => $"The {this.RaceName} race has:" + Environment.NewLine +
                $"Participants: {this.Pilots.Count}" + Environment.NewLine +
                $"Number of laps: {this.NumberOfLaps}" + Environment.NewLine +
                $"Took place: {(TookPlace ? "Yes" : "No")}";
    }
}
