using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository formulaOneCarRepository;

        public Controller()
        {
            this.pilotRepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.formulaOneCarRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            if (this.pilotRepository.FindByName(pilotName) is null || !(this.pilotRepository.FindByName(pilotName).Car is null))
            {
                throw new ArgumentException($"Pilot {pilotName} does not exist or has a car.");
            }
            if (this.formulaOneCarRepository.FindByName(carModel) is null)
            {
                throw new NullReferenceException($"Car {carModel} does not exist.");
            }

            var carType = this.formulaOneCarRepository.FindByName(carModel).GetType().Name;
            this.pilotRepository.FindByName(pilotName).AddCar(this.formulaOneCarRepository.FindByName(carModel));
            this.formulaOneCarRepository.Remove(this.formulaOneCarRepository.FindByName(carModel));

            return $"Pilot {pilotName} will drive a {carType} {carModel} car.";
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            if (this.raceRepository.FindByName(raceName) is null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            if (this.pilotRepository.FindByName(pilotFullName) is null // pilot does not exist
                || this.pilotRepository.FindByName(pilotFullName).CanRace == false // pilot cannot race
                || this.raceRepository.FindByName(raceName).Pilots.Any(p => p.FullName == pilotFullName)) // pilot is already in the race
            {
                throw new InvalidOperationException($"Can not add pilot {pilotFullName} to the race.");
            }

            this.raceRepository.FindByName(raceName).AddPilot(this.pilotRepository.FindByName(pilotFullName));
            return $"Pilot {pilotFullName} is added to the {raceName} race.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (this.formulaOneCarRepository.Models.Any(m => m.Model == model))
            {
                throw new InvalidOperationException($"Formula one car {model} is already created.");
            }

            IFormulaOneCar car;
            switch (type)
            {
                case "Ferrari":
                    car = new Ferrari(model, horsepower, engineDisplacement);
                    break;
                case "Williams":
                    car = new Williams(model, horsepower, engineDisplacement);
                    break;
                default:
                    throw new InvalidOperationException($"Formula one car type {type} is not valid.");
            }

            this.formulaOneCarRepository.Add(car);
            return $"Car {type}, model {model} is created.";
        }

        public string CreatePilot(string fullName)
        {
            var pilot = new Pilot(fullName);

            if (!(this.pilotRepository.FindByName(fullName) is null))
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }

            this.pilotRepository.Add(pilot);
            return $"Pilot {fullName} is created.";
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var race = new Race(raceName, numberOfLaps);

            if (this.raceRepository.Models.Any(r => r.RaceName == raceName))
            {
                throw new InvalidOperationException($"Race {raceName} is already created.");
            }

            this.raceRepository.Add(race);
            return $"Race {raceName} is created.";
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pilot in pilotRepository.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var race in raceRepository.Models.Where(m => m.TookPlace == true))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().Trim();
        }

        public string StartRace(string raceName)
        {
            if (this.raceRepository.FindByName(raceName) is null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            if (this.raceRepository.FindByName(raceName).Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than three participants.");
            }
            if (this.raceRepository.FindByName(raceName).TookPlace == true)
            {
                throw new InvalidOperationException($"Can not execute race {raceName}.");
            }

            this.raceRepository.FindByName(raceName).TookPlace = true;
            var race = this.raceRepository.FindByName(raceName);
            int numberOfLaps = race.NumberOfLaps;

            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (var pilot in race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(numberOfLaps)))
            {
                if (counter == 3)
                {
                    break;
                }

                counter++;
                switch (counter)
                {
                    case 1:
                        pilot.WinRace();
                        sb.AppendLine($"Pilot {pilot.FullName} wins the {race.RaceName} race.");
                        break;
                    case 2:
                        sb.AppendLine($"Pilot {pilot.FullName} is second in the {race.RaceName} race.");
                        break;
                    case 3:
                        sb.AppendLine($"Pilot {pilot.FullName} is third in the {race.RaceName} race.");
                        break;
                }
            }

            return sb.ToString().Trim();
        }
    }
}
