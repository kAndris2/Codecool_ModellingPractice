using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Modelling_Practice
{
    class Car
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int MaxSpeed { get; set; }
        public bool Validity { get; set; }

        //FELADAT: Progam.cs-ből mikor meghívom át kell adni a DataManager példányt!
        public Car()
        {
            RandomProperty randp = new RandomProperty();
            DataManager data = new DataManager();
            List<string[]> database = new List<string[]>();

            try
            {
                database = data.Import_Data("Cars.csv");
                while (true)
                {
                    string temp = randp.SetLicensePlate();
                    if (!Common.CheckValidPlate(database, temp))
                    {
                        LicensePlate = temp;
                        break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                LicensePlate = randp.SetLicensePlate();
            }

            Brand = randp.SetBrand();
            Color = randp.SetColor();
            MaxSpeed = randp.SetMaxSpeed();
            Validity = randp.SetValidity();
        }

        //FELADAT: public Car(Dictionary<string, string> data)
        public Car(string[] data)
        {
            /*
            PropertyInfo[] properties = typeof(Car).GetProperties();

            foreach (PropertyInfo prop in properties)
            {
               prop.SetValue(this, data[prop.Name]);
            }
            */
          
            foreach (string item in data)
            {
                if (LicensePlate == null)
                {
                    LicensePlate = item.ToUpper();
                    continue;
                }
                else if (Brand == null)
                {
                    Brand = Common.Capitalize(item);
                    continue;
                }
                else if (Color == null)
                {
                    Color = Common.Capitalize(item);
                    continue;
                }
                else if (MaxSpeed == 0)
                {
                    MaxSpeed = Convert.ToInt32(item);
                    continue;
                }
                else if (Validity == false)
                {
                    if (item.ToLower() == "yes" || item.ToLower() == "true")
                        Validity = true;
                    else
                        Validity = false;
                }
            }
        }
    }
}
