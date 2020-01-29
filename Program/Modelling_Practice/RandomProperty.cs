using System;
using System.Collections.Generic;
using System.Text;

namespace Modelling_Practice
{
    class RandomProperty
    {
        private static Random random = new Random();

        public string SetLicensePlate()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string plate = "";
            for (int i = 0; i < 7; i++)
            {
                if (i <= 2)
                    plate += chars[random.Next(chars.Length)];
                else if (i == 3)
                    plate += "-";
                else
                    plate += random.Next(0, 9);
            }
            return plate;
        }

        public string SetBrand()
        {
            DataManager data = new DataManager();
            string[] brand_list = data.Import_Data("Brands.csv")[0];
            return brand_list[random.Next(brand_list.Length)];
        }

        public string SetColor()
        {
            var colors = new List<string>() {"Black", "White", "Red", "Pink", "Green", "Gray", "Gold", "Silver",
                                            "Yellow", "Orange", "Blue", "Brown", "Purple", "Lime"};
            return colors[random.Next(colors.Count)];
        }

        public int SetMaxSpeed()
        {
            return random.Next(150, 300);
        }

        public bool SetValidity()
        {
            return random.Next() > (Int32.MaxValue / 2);
        }
    }
}
