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
        
        //public abstract void AddCar(Car car);

        //public abstract void DoRace();

        protected abstract List<Car> CarSelection(List<Car> cars);

        protected Car Start(List<Car> cars)
        {
            if (cars.Count == 0)
                throw new NullReferenceException("None of these cars meet the requirements!");

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

        protected virtual void GetRaceDescription()
        {
            Console.WriteLine($"\n[DESCRIPTION]:\n" +
                              $" - Validity = {Validity}\n" +
                              $" - Max participants = {MaxParticipant}\n" +
                              $" - Minimum speed = {MinimumSpeed}Km/h");
        }
    }
}
