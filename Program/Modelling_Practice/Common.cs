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

        public static bool CheckValidPlate(List<string[]> table, string plate)
        {
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i][0].Equals(plate))
                        return true;
                }
            }
            return false;
        }
    }
}
