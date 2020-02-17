using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    class Derby : Race
    { 
        public Derby()
        {
            Validity = "Invalid";
            MaxParticipant = 10;
        }

        public override void AddCar(Car car)
        {
            bool check = Validity.Equals("Valid");

            if (Cars.Count != MaxParticipant)
            {
                if (car.Validity == check)
                    Cars.Add(car);
                else
                    throw new ArgumentException("This car doesn't meet with the requirments! - The car's validity is Valid!");
            }
            else
                throw new ArgumentException("You can't add this car to the race because it's full!");
        }
    }
}
