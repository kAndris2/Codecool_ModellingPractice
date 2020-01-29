using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Derby : Race
    { 
        public Derby(List<Car> cars)
        {
            Winner = Start(CarSelection(cars));
        }

        protected override List<Car> CarSelection(List<Car> cars)
        {
            List<Car> selection = new List<Car>();

            foreach (Car car in cars)
            {
                if (car.Validity == Validity & car.MaxSpeed >= MinimumSpeed)
                {
                    selection.Add(car);
                }
            }

            return selection;
        }
    }
}
