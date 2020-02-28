using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace api
{
    class DataManager
    {
        private const string FILENAME = "Cars.xml";
        private readonly List<Car> Cars = new List<Car>();
        private RandomProperty RandProp;

        public DataManager()
        {
            RandProp = new RandomProperty();

            try
            {
                Cars.AddRange(Import());
            }
            catch (FileNotFoundException)
            {
                new ConsoleLogger().Info($"The database is empty! - File not found! ('{FILENAME}')\n");
            }
        }

        public List<Car> GetCars() { return Cars; }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public Car AddNewRandomCar()
        {
            Car car = new Car();

            while (true)
            {
                string temp = RandProp.SetLicensePlate();
                if (!Common.CheckValidPlate(Cars, temp))
                {
                    car.LicensePlate = temp;
                    break;
                }
            }

            car.Brand = RandProp.SetBrand();
            car.Color = RandProp.SetColor();
            car.MaxSpeed = RandProp.SetMaxSpeed();
            car.Validity = RandProp.SetValidity();

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
        public RandomProperty GetRP() { return RandProp; }
    }
}