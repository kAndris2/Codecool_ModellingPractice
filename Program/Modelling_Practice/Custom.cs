using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class Custom : Race
    {
        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public List<Car> GetRaceCars() { return Cars; }

        protected override List<Car> CarSelection(List<Car> cars) { return null; }
    }
}
