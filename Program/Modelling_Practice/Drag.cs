using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Drag : Race
    {
        public Drag()
        {
            Validity = "Valid";
            MaxParticipant = 3;
            MinimumSpeed = 260;
        }

        public override void AddCar(Car car)
        {
            bool check = Validity.Equals("Valid");

            if (Cars.Count != MaxParticipant)
            {
                if (car.MaxSpeed >= MinimumSpeed)
                {
                    if (car.Validity == check)
                        Cars.Add(car);
                    else
                        throw new ArgumentException("This car doesn't meet with the requirments! - The car's validity is Invalid!");
                }
                else
                    throw new ArgumentException($"This car doesn't meet with the requirments! - Too slow ('{car.MaxSpeed}km/h / {MinimumSpeed}km/h')");
            }
            else
                throw new ArgumentException("You can't add this car to the race because it's full!");
        }
    }
}
