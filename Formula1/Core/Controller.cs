using Formula1.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        public string AddCarToPilot(string pilotName, string carModel)
        {
            throw new NotImplementedException();
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            throw new NotImplementedException();
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            throw new NotImplementedException();
        }

        public string CreatePilot(string fullName)
        {
            throw new NotImplementedException();
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            throw new NotImplementedException();
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
