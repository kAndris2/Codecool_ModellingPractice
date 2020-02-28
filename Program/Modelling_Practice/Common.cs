using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    static class Common
    {
        public static string Capitalize(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            string[] temp = str.Split(" ");
            for (int i = 0; i < temp.Length; i++)
                temp[i] = char.ToUpper(temp[i][0]) + temp[i].Substring(1).ToLower();

            return string.Join(" ", temp);
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

        public static bool CheckPlateFormat(string input)
        {
            if (input.Length == 7)
            {
                for (int n = 0; n < input.Length; n++)
                {
                    if (n <= 2 & !char.IsLetter(input[n]) ||
                         input[3] != '-' || n >= 4 & !char.IsNumber(input[n]))
                    {
                        return false;
                    }
                }
            }
            else
                return false;
            return true;
        }
    }
}
