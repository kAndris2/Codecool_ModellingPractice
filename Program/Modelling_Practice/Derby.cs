using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Derby : Race
    { 
        public Derby(List<Car> cars)
        {
            Validity = false;
            MaxParticipant = 10;
            GetRaceDescription();
            Winner = Start(CarSelection(cars));
            ShowParticipantsAndWinner();
        }

        protected override List<Car> CarSelection(List<Car> cars)
        {
            List<Car> selection = new List<Car>();

            foreach (Car car in cars)
            {
                if (car.Validity == Validity)
                {
                    selection.Add(car);
                }
            }

            return selection;
        }
    }
}
