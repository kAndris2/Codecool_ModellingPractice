using System;
using System.Collections.Generic;
using System.IO;

namespace Modelling_Practice
{
    class Program
    {
        public const string FILENAME = "Cars.csv";

        static void Main(string[] args)
        {
            DataManager data = new DataManager();
            ConsoleLogger logger = new ConsoleLogger();
            List<Car> car = new List<Car>();
            //FELADAT: Ez legyen dictionary a Reflection miatt!
            //List<Dictionary<string, string>> database = new List<Dictionary<string, string>>();
            List<string[]> database = new List<string[]>();

            try
            {
                database.AddRange(data.Import_Data(FILENAME));
                for (int i = 0; i < database.Count; i++)
                {
                    car.Add(new Car(database[i]));
                }
            }
            catch (FileNotFoundException e)
            {
                logger.Info($"The database is empty! - {e.Message}\n");
            }

            while (true)
            {
                HandleMenu();
                try
                {
                    if (!Choose(car, logger, data))
                        break;
                    else
                    {
                        Console.WriteLine("\n--->[Press enter to continue.]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (KeyNotFoundException e) { ManageException(logger, e.Message); }
                catch (EmptyDatabaseException e) { ManageException(logger, e.Message); }
                catch (InvalidInputException e) { ManageException(logger, e.Message); }
                catch (NullReferenceException e) { ManageException(logger, e.Message); }
            }
        }
        public static void HandleMenu()
        {
            Console.WriteLine("Simulation commands:\n");
            List<string> options = new List<string>
            {
                ":fill    - Fills the database with randomly generated cars.",
                ":create  - Create a specific car.",
                ":list    - List already exist cars.",
                ":find    - Get car info by license plate.",
                ":update  - Update car.",
                ":remove  - Delete car.",
                ":save    - Saves the simulation to a file.",
                ":race    - Make a race."
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i+1}). - {options[i]}");
            Console.WriteLine($"\n(0). - :exit    - Exit.");
        }

        public static bool Choose(List<Car> instance, ConsoleLogger logger, DataManager data)
        {
            Console.WriteLine("\nPlease enter a command or use the hotkeys: ");
            string option = Console.ReadLine();

            if (option == ":exit" || option == "0")
            {
                if (!File.Exists(FILENAME))
                    return false;

                if (!EqualArrays(GetProperties(instance), data.Import_Data(FILENAME)))
                {
                    Console.Clear();
                    logger.Warning("You didn't saved your database yet!\nYou really want to quit? (yes/no)");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        Environment.Exit(-1);
                        return false;
                    }
                    else
                        return true;
                }
                else
                    Environment.Exit(-1);
                return false;
            }
            else if (option == ":fill" || option == "1")
            {
                Console.Clear();
                Console.WriteLine("How many cars you want to create?");
                int num = int.Parse(Console.ReadLine());

                List<Car> cars = new List<Car>();
                for (int i = 0; i < num; i++)
                {
                    cars.Add(new Car());
                }

                Console.Clear();
                logger.Info($"You have created {num} pieces of cars.\n");
                foreach (Car car in cars)
                {
                    instance.Add(car);
                    Console.WriteLine(PrintCarProperties(car, false));
                }
                return true;
            }
            else if (option == ":create" || option == "2")
            {
                Console.Clear();
                string[] properties = { "license plate. (e.g.: XXX-000):",
                                        "brand.:",
                                        "color.:",
                                        "max speed.:",
                                        "Can the car take part in the traffic? (Yes/No)"
                                     };
                //FELADAT: Ez legyen dictionary, hogy a Reflection működhessen!
                string[] car_data = new string[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    if (i == properties.Length - 1)
                        Console.WriteLine($"\n{properties[i]}");
                    else
                        Console.WriteLine($"\nEnter your car {properties[i]}");

                    if (i == 0)
                    {
                        while (true)
                        {
                            bool check = true;
                            string temp = Console.ReadLine().ToUpper();
                            if (temp.Length == 7)
                            {
                                for (int n = 0; n < temp.Length; n++)
                                {
                                    if (n <= 2 & !char.IsLetter(temp[n]) ||
                                         temp[3] != '-' || n >= 4 & !char.IsNumber(temp[n]))
                                    {
                                        check = false;
                                    }
                                }
                            }
                            else
                                check = false;

                            if (check)
                            {
                                if (!Common.CheckValidPlate(instance, temp))
                                {
                                    car_data[i] = temp;
                                    break;
                                }
                                else
                                {
                                    Console.Clear();
                                    logger.Error($"Your license plate is already exist! ('{temp}')");
                                    Console.WriteLine("Please enter another license plate:");
                                }
                            }
                            else
                                throw new InvalidInputException($"Invalid license plate format! ('{temp}')");
                        }
                    }
                    else if (i == 3)
                    {
                        while (true)
                        {
                            string temp = Console.ReadLine();
                            bool check = true;
                            for (int n = 0; n < temp.Length; n++)
                            {
                                if (!char.IsNumber(temp[n]))
                                {
                                    Console.Clear();
                                    logger.Error($"The entered value is not a number! ('{temp}')");
                                    Console.WriteLine("Please enter another value:");
                                    check = false;
                                    break;
                                }
                            }
                            if (check)
                            {
                                car_data[i] = temp;
                                break;
                            }
                        }
                    }
                    else
                        car_data[i] = Common.Capitalize(Console.ReadLine());
                }

                instance.Add(new Car(car_data));
                Console.Clear();
                logger.Info("You have created a car.\n");
                Console.WriteLine(PrintCarProperties(instance[instance.Count - 1], true));

                return true;
            }
            else if (option == ":list" || option == "3")
            {
                if (instance.Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                logger.Info($"There are {instance.Count} cars in the database.\n");
                foreach (Car car in instance)
                {
                    Console.WriteLine(PrintCarProperties(car, false));
                }

                return true;
            }
            else if (option == ":find" || option == "4")
            {
                if (instance.Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                Console.WriteLine("What do you want to look for?\n");
                string[] properties = {
                                        "License plate",
                                        "Brand",
                                        "Color",
                                        "Max speed",
                                        "Validity"
                                        };
                for (int i = 0; i < properties.Length; i++)
                {
                    Console.WriteLine($"({i + 1}). - {properties[i]}");
                }
                Console.WriteLine("\nPlease type a property index to choose it.");
                int index;
                string input = Console.ReadLine();
                if (int.TryParse(input, out index))
                    index = int.Parse(input) - 1;
                else
                    throw new InvalidInputException($"Invalid type - string != int! ('{input}')");

                Console.Clear();
                Console.WriteLine($"Please enter the {properties[index].ToLower()} what are you looking for.");
                string search = "";
                if (index == 0)
                    search = Console.ReadLine().ToUpper();
                else
                    search = Common.Capitalize(Console.ReadLine());

                Console.Clear();
                int count = 0;
                foreach (Car car in instance)
                {
                    if (index == 0 && car.LicensePlate.Equals(search) ||
                        index == 1 && car.Brand.Equals(search) ||
                        index == 2 && car.Color.Equals(search) ||
                        index == 3 && car.MaxSpeed.ToString().Equals(search) ||
                        index == 4 && car.Validity.ToString().Equals(search))
                    {
                        count++;
                        Console.WriteLine(PrintCarProperties(car, false));
                    }
                }
                logger.Info($"Totally matches: {count}pc");

                return true;
            }
            else if (option == ":update" || option == "5")
            {
                Console.Clear();
                Console.WriteLine("Enter the car's license plate:");
                string plate = Console.ReadLine().ToUpper();
                if (!Common.CheckValidPlate(instance, plate))
                    throw new InvalidInputException($"Invalid license plate! ('{plate}')");

                Console.Clear();
                string[] properties = new string[] {
                                                    "License plate",
                                                    "Brand",
                                                    "Color",
                                                    "Max speed",
                                                    "Validity"
                                                   };
                Console.WriteLine("Please type a property name to choose it.\n");
                for (int i = 0; i < properties.Length; i++)
                {
                    Console.WriteLine($"- {properties[i]}");
                }
                Console.WriteLine("\nWhich property you want to change?");
                string choose = Console.ReadLine().ToLower();

                Console.Clear();
                Console.WriteLine($"What will be the new {choose}?");

                foreach (Car car in instance)
                {
                    if (car.LicensePlate.Equals(plate))
                    {
                        if (choose == "license plate")
                            car.LicensePlate = Console.ReadLine();
                        else if (choose == "brand")
                            car.Brand = Common.Capitalize(Console.ReadLine());
                        else if (choose == "color")
                            car.Color = Common.Capitalize(Console.ReadLine());
                        else if (choose == "max speed")
                            car.MaxSpeed = int.Parse(Console.ReadLine());
                        else if (choose == "validity")
                            car.Validity = bool.Parse(Console.ReadLine());

                        Console.Clear();
                        logger.Info("You have succesfully updated the car's property.\n");
                        Console.WriteLine(PrintCarProperties(car, true));
                        break;
                    }
                }

                return true;
            }
            else if (option == ":remove" || option == "6")
            {
                if (instance.Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                Console.WriteLine("Enter the car's license plate:");
                string plate = Console.ReadLine().ToUpper();
                if (!Common.CheckValidPlate(instance, plate))
                    throw new InvalidInputException($"Invalid license plate! ('{plate}')");
                Console.Clear();
                //
                int index = -1;
                foreach (Car car in instance)
                {
                    index++;
                    if (plate.Equals(car.LicensePlate))
                    {
                        logger.Info("You have removed a car from a database.\n");
                        Console.WriteLine(PrintCarProperties(car, true));
                        break;
                    }
                }

                instance.RemoveAt(index);
                return true;
            }
            else if (option == ":save" || option == "7")
            {
                Console.Clear();
                List<string[]> table = GetProperties(instance);
                data.Export_Data(FILENAME, table);
                logger.Info("You have successfully saved your simulation");

                return true;
            }
            else if (option == ":race" || option == "8")
            {
                Console.Clear();
                List<string> types = new List<string>() { 
                                                            "Illegal race",
                                                            "Derby",
                                                            "Drag race",
                                                            "Custom race" //+Menü
                                                        };
                for (int i = 0; i < types.Count; i++)
                    Console.WriteLine($"{i + 1}. {types[i]}");

                Console.WriteLine("\nSelect a race type by it's index.:");
                string index = Console.ReadLine();

                if (!int.TryParse(index, out int x))
                    throw new InvalidInputException($"The entered value is not a number! ('{index}')");
                else if (int.Parse(index) > types.Count || int.Parse(index) < 0)
                    throw new KeyNotFoundException($"There is no such option! ('{index}')");

                Console.Clear();
                logger.Info($"You have started {types[int.Parse(index) - 1]}.");
                Race race = null;

                if (index == "1") { race = new IllegalRace(instance); }
                else if (index == "2") race = new Derby(instance);
                else if (index == "3") race = new Drag(instance);

                Console.WriteLine($"\nParticipants: {race.Cars.Count}/{race.MaxParticipant}");
                foreach (Car car in race.Cars)
                {
                    Console.WriteLine(PrintCarProperties(car, false));
                }
                Console.WriteLine($"\nThe winner is {race.Winner.LicensePlate} - {race.Winner.Brand}!");

                return true;
            }
            else
                throw new KeyNotFoundException($"There is no such option! ('{option}')");
        }

        public static List<string[]> GetProperties(List<Car> table)
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

        public static string PrintCarProperties(Car car, bool check)
        {
            string text = "";
            if (check)
                text = $"License plate: {car.LicensePlate}\n" +
                                  $"Brand: {car.Brand}\n" +
                                  $"Color: {car.Color}\n" +
                                  $"Max speed: {car.MaxSpeed}Km/h\n" +
                                  $"Validity: {car.Validity}";
            else
            {

                text = $"{CorrectString(car.LicensePlate, 0)} | " +
                       $"{CorrectString(car.Brand, 12)} | " +
                       $"{CorrectString(car.Color, 6)} | " +
                       $"{CorrectString(car.MaxSpeed.ToString() + "Km/h", 0)} | " +
                       $"{CorrectString(car.Validity.ToString(), 0)}";
            }

            return text;
        }

        public static bool EqualArrays(List<string[]> table1, List<string[]> table2)
        {
            if (table1.Count.Equals(table2.Count))
            {
                for (int i = 0; i < table1.Count; i++)
                {
                    string[] item1 = table1[i];
                    string[] item2 = table2[i];
                    for (int n = 0; n < item1.Length; n++)
                    {
                        if (!item1[n].Equals(item2[n]))
                            return false;
                    }
                }
            }
            else
                return false;
            return true;
        }

        public static void ManageException(ConsoleLogger logger, string message)
        {
            Console.Clear();
            logger.Error(message);
        }

        public static string CorrectString(string element, int num)
        {
            num = num - element.Length;
            for (int i = 0; i < num; i++)
                element += " ";
            return element;
        }
    }
}
