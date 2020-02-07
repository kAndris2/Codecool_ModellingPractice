using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    abstract class Race
    {
        virtual public int MinimumSpeed { get; protected set; }
        virtual public int MaxParticipant { get; protected set; }
        virtual public string Validity { get; protected set; }
        protected Car Winner { get; set; }
        protected List<Car> Cars = new List<Car>();

        public abstract void AddCar(Car car);

        public List<Car> GetRaceCars() { return Cars; }
        public Car GetWinner() { return Winner; }

        public void DoRace()
        {
            Random rand = new Random();
            Winner = Cars[rand.Next(Cars.Count)];
        }

        public bool Contains(Car car)
        {
            foreach (Car item in Cars)
                if (item.Equals(car))
                    return true;
            return false;
        }
    }
}
