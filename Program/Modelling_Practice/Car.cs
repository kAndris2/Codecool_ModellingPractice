using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace api
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int MaxSpeed { get; set; }
        public bool Validity { get; set; }
        public bool Choosed { get; set; }

        public Car() { }

        public Car(string[] data)
        {
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

        public override string ToString()
        {
            return $"{LicensePlate} - {Brand}";
        }

        public override bool Equals(Object car)
        {
            if (LicensePlate == ((Car)car).LicensePlate )
                return true;
            return false;
        }
    }
}
