using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    abstract class Race
    {
        virtual protected int MinimumSpeed { get; set; }
        virtual protected bool Validity { get; set; }
        virtual public int MaxParticipant { get; set; }
        public Car Winner { get; protected set; }
        public List<Car> Cars { get; protected set; }
        
        public abstract void AddCar(Car car);

        public abstract void DoRace();

























        protected abstract List<Car> CarSelection(List<Car> cars);

        protected Car Start(List<Car> cars)
        {
            Random rand = new Random();

            if (cars.Count > MaxParticipant)
            {
                for (int i = 0; i < cars.Count - MaxParticipant; i ++)
                {
                    cars.RemoveAt(rand.Next(cars.Count));
                }
            }

            Cars = cars;

            return cars[rand.Next(cars.Count)];
        }

    }
}
