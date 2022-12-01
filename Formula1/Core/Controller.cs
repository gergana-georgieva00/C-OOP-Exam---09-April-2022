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

            this.pilotRepository.FindByName(pilotName).AddCar(this.formulaOneCarRepository.FindByName(carModel));
            this.formulaOneCarRepository.Remove(this.formulaOneCarRepository.FindByName(carModel));

            return $"Pilot {pilotName} will drive a {this.formulaOneCarRepository.FindByName(carModel).GetType().Name} {carModel} car.";
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            if (!(this.raceRepository.FindByName(raceName) is null))
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            if (this.pilotRepository.FindByName(pilotFullName) is null || !(this.pilotRepository.FindByName(pilotFullName).Car is null)
                || this.pilotRepository.FindByName(pilotFullName).CanRace == false)
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
            throw new NotImplementedException();
        }

        public string RaceReport()
        {
            throw new NotImplementedException();
        }

        public string StartRace(string raceName)
        {
            throw new NotImplementedException();
        }
    }
}
