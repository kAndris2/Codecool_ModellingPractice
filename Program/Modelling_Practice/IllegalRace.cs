using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class IllegalRace : Race
    {
        public IllegalRace(List<Car> cars)
        {
            MaxParticipant = 15;
            MinimumSpeed = 220;
            GetRaceDescription();
            Winner = Start(CarSelection(cars));
        }

        protected override List<Car> CarSelection(List<Car> cars)
        {
            List<Car> selection = new List<Car>();

            foreach (Car car in cars)
            {
                if (car.MaxSpeed >= MinimumSpeed)
                {
                    selection.Add(car);
                }
            }

            return selection;
        }

        protected override void GetRaceDescription()
        {
            Console.WriteLine($"\n[DESCRIPTION]:\n" +
                              $" - Validity = N/A\n" +
                              $" - Max participants = {MaxParticipant}\n" +
                              $" - Minimum speed = {MinimumSpeed}Km/h");
        }
    }
}
