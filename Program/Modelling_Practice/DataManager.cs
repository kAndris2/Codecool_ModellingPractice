using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Modelling_Practice
{
    class DataManager
    {
        private static readonly List<string> BRANDS = new List<string>(new string[] {
            "Alfa Romeo"
        });

        private List<Car> cars = new List<Car>();

        public DataManager()
        {
            // read all cars data and save it
        }

        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        public Car AddNewRandomCar()
        {
            Car car = new Car();
            // init all fields EXCEPT license plate
            // init license plate
            while (cars.Contains(car))
            {
                car.LicensePlate = ""; // reinit random
            }
            return car;
        }

        public void Save()
        {

        }

        //FELADAT: Private-re!!
        public List<string[]> Import_Data(String filename)
        {
            if (File.Exists(filename))
            {
                string[] data = File.ReadAllLines(filename);
                List<string[]> table = new List<string[]>();

                for (int i = 0; i < data.Length; i++)
                {
                    table.Add(data[i].Split(';'));
                }

                return table;
            }
            else
            {
                throw new FileNotFoundException($"File not found! ('{filename}')");
            }
        }

        public void Export_Data(String filename, List<string[]> table)
        {
            string text = "";
            for (int i = 0; i < table.Count; i++)
            {
                text += string.Join(";", table[i]) + "\n";
            }
            File.WriteAllText(filename, text);
        }
    }
}