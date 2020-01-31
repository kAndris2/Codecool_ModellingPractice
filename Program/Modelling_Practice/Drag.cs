using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Drag : Race
    {
        public Drag(List<Car> cars)
        {
            Validity = true;
            MaxParticipant = 3;
            MinimumSpeed = 260;
            GetRaceDescription();
            Winner = Start(CarSelection(cars));
            ShowParticipantsAndWinner();
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
