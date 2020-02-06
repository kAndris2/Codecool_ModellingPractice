using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Custom : Race
    {
        public void AddCars(List<Car> car)
        {
            Cars = car;
        }

        public void DoRace()
        {
            MaxParticipant = Cars.Count;
            GetRaceDescription();
            Winner = Start(Cars);
            ShowParticipantsAndWinner();
        }

        protected override void GetRaceDescription()
        {
            Console.WriteLine($"\n[DESCRIPTION]:\n" +
                              $" - Validity = N/A\n" +
                              $" - Max participants = N/A\n" +
                              $" - Minimum speed = N/A");
        }

        protected override List<Car> CarSelection(List<Car> cars) { return null; }
    }
}
