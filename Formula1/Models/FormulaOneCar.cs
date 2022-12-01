using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsePower;
        private double engineDisplacement;

        public FormulaOneCar(string model, int horsePower, double engineDisplacement)
        {
            this.Model = model;
            this.Horsepower = horsePower;
            this.EngineDisplacement = engineDisplacement;
        }

        public string Model 
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new ArgumentException($"Invalid car model: {value}.");
                this.model = value;
            } 
        }

        public int Horsepower
        {
            get => this.horsePower;
            private set
            {
                if (value < 900 || value > 1050)
                    throw new ArgumentException($"Invalid car horsepower: {value}.");
                this.horsePower = value;
            }
        }

        public double EngineDisplacement
        {
            get => this.engineDisplacement;
            private set
            {
                if (value < 1.6 || value > 2)
                    throw new ArgumentException($"Invalid car engine displacement: {value}.");
                this.engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
        => this.EngineDisplacement / this.Horsepower * laps;
    }
}
