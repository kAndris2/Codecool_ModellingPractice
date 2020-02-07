using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    static class Common
    {
        public static string Capitalize(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public static bool CheckValidPlate(List<Car> table, string plate)
        {
            if (table.Count >= 1)
            {
                foreach (Car car in table)
                {
                    if (car.LicensePlate.Equals(plate))
                        return true;
                }
            }
            return false;
        }
    }
}
