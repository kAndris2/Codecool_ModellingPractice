﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Modelling_Practice
{
    class DataManager
    {
        public const string FILENAME = "Cars.xml";
        private readonly List<Car> Cars = new List<Car>();

        public DataManager()
        {
            ConsoleLogger logger = new ConsoleLogger();

            try
            {
                Cars.AddRange(Import());
            }
            catch (FileNotFoundException)
            {
                logger.Info($"The database is empty! - File not found! ('{FILENAME}')\n");
            }
        }

        public List<Car> GetCars() { return Cars; }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public Car AddNewRandomCar()
        {
            RandomProperty randp = new RandomProperty();
            Car car = new Car();

            while (true)
            {
                string temp = randp.SetLicensePlate();
                if (!Common.CheckValidPlate(Cars, temp))
                {
                    car.LicensePlate = temp;
                    break;
                }
            }

            car.Brand = randp.SetBrand();
            car.Color = randp.SetColor();
            car.MaxSpeed = randp.SetMaxSpeed();
            car.Validity = randp.SetValidity();

            return car;
        }

        private List<Car> Import()
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<Car>));
            List<Car> i;
            using (FileStream readfile = File.OpenRead(FILENAME))
            {
                i = (List<Car>)reader.Deserialize(readfile);
            }
            return i;
        }

        public void Save()
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Car>));

            using (TextWriter writerfinal = new StreamWriter(FILENAME))
            {
                writer.Serialize(writerfinal, Cars);
            }
        }

        /*
        public void Save()
        {
            List<string[]> table = GetProperties(Cars);

            string text = "";
            for (int i = 0; i < table.Count; i++)
            {
                text += string.Join(";", table[i]) + "\n";
            }
            File.WriteAllText(FILENAME, text);
        }
        */

        private List<string[]> GetProperties(List<Car> table)
        {
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < table.Count; i++)
            {
                string[] temp = new string[] {
                                                table[i].LicensePlate,
                                                table[i].Brand,
                                                table[i].Color,
                                                table[i].MaxSpeed.ToString(),
                                                table[i].Validity.ToString()
                                             };
                result.Add(temp);
            }
            return result;
        }

        public bool EqualInstances(List<Car> table)
        {
            if (Cars.Count.Equals(table.Count))
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (!Cars[i].Equals(table[i]))
                        return false;
                }
            }
            else
                return false;
            return true;
        }

        public void DeleteCar(int index) { Cars.RemoveAt(index); }
    }
}