using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    abstract class Race
    {
        virtual protected int MinimumSpeed { get; set; }
        virtual protected bool Validity { get; set; }
        virtual protected int MaxParticipant { get; set; }
        protected Car Winner { get; set; }
        protected List<Car> Cars { get; set; }

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

        protected void ShowParticipantsAndWinner()
        {
            Console.WriteLine($"\nParticipants: {Cars.Count}/{MaxParticipant}");
            foreach (Car car in Cars)
            {
                Console.WriteLine(Common.PrintCarProperties(car, false));
            }
            Console.WriteLine($"\nThe winner is {Winner.LicensePlate} - {Winner.Brand}!");
        }
    }
}
