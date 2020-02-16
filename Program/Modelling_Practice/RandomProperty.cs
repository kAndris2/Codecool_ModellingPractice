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
            var brand_list = new List<string>() { "Fiat", "Nissan", "Toyota", "Mitsubishi", "Bmw", "Audi", "Mercedes", "Volkswagen", "Opel", "Mazda",
                                                  "Hyundai", "Hummer", "Corvette", "Crysler", "Ferrari", "Lamborghini", "Bugatti", "Koenigsegg", "Kia",
                                                  "Mclaren", "Trabant", "Jeep", "Volvo", "Subaru", "Mini", "Dodge", "Suzuki", "Porsche", "Acura", "Lada",
                                                  "Peugeot", "Renault", "Ford", "Jaguar", "Maserati", "Saab", "Honda", "Lexus", "Daewoo", "Lancia", "Bentley",
                                                  "Infiniti", "Citroen", "Aston Martin", "Alfa Romeo", "Gmc", "Cadillac", "Land Rover", "Pagani", "Rolls Royce",
                                                  "Tesla", "Dacia", "Chevrolet", "Lincoln", "Lotus", "Mg", "Seat", "Skoda", "Smart"};
            return brand_list[random.Next(brand_list.Count)];
        }

        public string SetColor()
        {
            var colors = new List<string>() {"Black", "White", "Red", "Pink", "Green", "Gray", "Gold", "Silver", "Coral", "Navy", "Amber", "Azure",
                                            "Yellow", "Orange", "Blue", "Brown", "Purple", "Lime", "Peach", "Aqua", "Indigo", "Maroon", "Orchid"};
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
