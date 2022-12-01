﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models.Contracts
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            this.FullName = fullName;
            this.CanRace = false;
        }

        public string FullName 
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException($"Invalid pilot name: {value}.");
                this.FullName = value;
            }
        }

        public IFormulaOneCar Car 
        {
            get => this.car;
            private set
            {
                if (value is null)
                    throw new NullReferenceException("Pilot car can not be null.");
                this.car = value;
            }
        }

        public int NumberOfWins { get; private set; }

        public bool CanRace { get; private set; }

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }

        public override string ToString()
            => $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
    }
}
