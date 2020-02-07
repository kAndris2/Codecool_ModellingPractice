using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class IllegalRace : Race
    {
        public IllegalRace()
        {
            MaxParticipant = 15;
            MinimumSpeed = 220;
        }

        public override void AddCar(Car car)
        {
            if (Cars.Count != MaxParticipant)
            {
                if (car.MaxSpeed >= MinimumSpeed)
                    Cars.Add(car);
                else
                    throw new ArgumentException($"This car doesn't meet with the requirments! - Too slow ('{car.MaxSpeed}km/h / {MinimumSpeed}km/h')");
            }
            else
                throw new ArgumentException("You can't add this car to the race because it's full!");
        }
    }
}
