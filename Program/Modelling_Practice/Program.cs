using System;
using System.Collections.Generic;
using System.IO;
using api;

namespace cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager data = new DataManager();
            ConsoleLogger logger = new ConsoleLogger();
            List<Car> cars = new List<Car>();

            foreach (Car car in data.GetCars())
                cars.Add(car);

            while (true)
            {
                HandleMenu();
                try
                {
                    if (!Choose(cars, logger, data))
                        break;
                    else
                    {
                        Console.WriteLine("\n--->[Press enter to continue.]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (MyExceptions e) { ManageException(logger, e.Message); }
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
                ":find    - Get car info.",
                ":update  - Update car.",
                ":remove  - Delete car.",
                ":clear   - Removes all elements from the database.",
                ":save    - Saves the simulation to a file.",
                ":reload  - Restores the database to boot state.",
                ":race    - Make a race."
            };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine(options[i]);
            Console.WriteLine("\n:exit    - Exit.");
        }

        public static bool Choose(List<Car> original, ConsoleLogger logger, DataManager data)
        {
            Console.WriteLine($"\nPlease enter a command to choose a function:");
            string option = Console.ReadLine();

            if (option == ":exit")
            {
                if (!data.EqualInstances(original))
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
            else if (option == ":fill")
            {
                Console.Clear();
                Console.WriteLine("How many cars you want to create?");
                string num = Console.ReadLine();

                if (!int.TryParse(num, out int x))
                    throw new InvalidInputException($"The entered value is not a number! ('{num}')");

                if (num == "0")
                    throw new SWWException("The value cannot be 0!");

                List<Car> cars = new List<Car>();
                for (int i = 0; i < int.Parse(num); i++)
                {
                    cars.Add(data.AddNewRandomCar());
                }

                Console.Clear();
                logger.Info($"You have created {num} pieces of cars.\n");
                bool check = original.Count == 0;

                foreach (Car car in cars)
                {
                    data.AddCar(car);
                    /*
                    if (check)
                        original.Add(car);
                    */
                    Console.WriteLine(PrintCarProperties(car, false));
                }
                return true;
            }
            else if (option == ":create")
            {
                Console.Clear();
                string[] properties = { "license plate. (e.g.: XXX-000):",
                                        "brand.:",
                                        "color.:",
                                        "max speed.:",
                                        "Can the car take part in the traffic? (Yes/No)"
                                     };
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
                            string temp = Console.ReadLine().ToUpper();

                            if (Common.CheckPlateFormat(temp))
                            {
                                if (!Common.CheckValidPlate(data.GetCars(), temp))
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

                Car car = new Car(car_data);

                data.AddCar(car);
                if (original.Count == 0)
                    original.Add(car);

                Console.Clear();
                logger.Info("You have created a car.\n");
                Console.WriteLine(PrintCarProperties(car, true));

                return true;
            }
            else if (option == ":list")
            {
                if (data.GetCars().Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                logger.Info($"There are {data.GetCars().Count} cars in the database.\n");
                foreach (Car car in data.GetCars())
                {
                    Console.WriteLine(PrintCarProperties(car, false));
                }

                return true;
            }
            else if (option == ":find")
            {
                if (data.GetCars().Count == 0)
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
                string search;

                switch(index)
                {
                    case 0:
                        search = Console.ReadLine().ToUpper();
                        if (!Common.CheckPlateFormat(search))
                            throw new InvalidInputException($"Invalid license plate format! ('{search}')");
                        break;
                    case 1:
                        search = Common.Capitalize(Console.ReadLine());
                        if (!data.GetRP().GetBrands().Contains(search))
                            throw new SWWException($"Unknown brand! ('{search}')");
                        break;
                    case 2:
                        search = Common.Capitalize(Console.ReadLine());
                        if (!data.GetRP().GetColors().Contains(search))
                            throw new SWWException($"Unknown color! ('{search}')");
                        break;
                    case 3:
                        search = Console.ReadLine();
                        if (!int.TryParse(search, out int x))
                            throw new InvalidInputException($"The entered value is not a number! ('{search}')");
                            break;
                    case 4:
                        search = Console.ReadLine().ToLower();
                        if (search != "valid" && search != "invalid")
                            throw new InvalidInputException($"Invalid validity! ('{search}')");
                        break;
                    default:
                        search = Console.ReadLine();
                        break;
                }

                Console.Clear();
                int count = 0;
                foreach (Car car in data.GetCars())
                {
                    if (index == 0 && car.LicensePlate.Equals(search) ||
                        index == 1 && car.Brand.Equals(search) ||
                        index == 2 && car.Color.Equals(search) ||
                        index == 3 && car.MaxSpeed.ToString().Equals(search) ||
                        index == 4 && car.Validity.ToString().Equals(search.Equals("valid").ToString()))
                    {
                        count++;
                        Console.WriteLine(PrintCarProperties(car, false));
                    }
                }
                logger.Info($"Totally matches: {count}pc");

                return true;
            }
            else if (option == ":update")
            {
                if (data.GetCars().Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                Console.WriteLine("Enter the car's license plate:");
                string plate = Console.ReadLine().ToUpper();

                if (!Common.CheckPlateFormat(plate))
                    throw new InvalidInputException($"Invalid license plate format! ('{plate}')");
                if (!Common.CheckValidPlate(data.GetCars(), plate))
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

                if (!Array.Exists(properties, item => item == Common.Capitalize(choose)))
                    throw new UnknownKeyException($"There is no such option! ('{choose}')");

                Console.Clear();
                Console.WriteLine($"What will be the new {choose}?");

                foreach (Car car in data.GetCars())
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
                            car.Validity = Console.ReadLine().ToLower().Equals("valid");

                        Console.Clear();
                        logger.Info($"You have succesfully updated the car's {choose}.\n");
                        Console.WriteLine(PrintCarProperties(car, true));
                        break;
                    }
                }

                return true;
            }
            else if (option == ":remove")
            {
                if (data.GetCars().Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                Console.WriteLine("Enter the car's license plate:");
                string plate = Console.ReadLine().ToUpper();

                if (!Common.CheckPlateFormat(plate))
                    throw new InvalidInputException($"Invalid license plate format! ('{plate}')");
                if (!Common.CheckValidPlate(data.GetCars(), plate))
                    throw new InvalidInputException($"Invalid license plate! ('{plate}')");
                Console.Clear();
                //
                int index = -1;
                foreach (Car car in data.GetCars())
                {
                    index++;
                    if (plate.Equals(car.LicensePlate))
                    {
                        logger.Info("You have removed a car from a database.\n");
                        Console.WriteLine(PrintCarProperties(car, true));
                        break;
                    }
                }

                data.DeleteCar(index);
                return true;
            }
            else if (option == ":clear")
            {
                if (data.GetCars().Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                logger.Info($"You have successfully delete '{data.GetCars().Count}' cars from the database.");
                data.GetCars().Clear();

                return true;
            }
            else if (option == ":save")
            {
                if (data.GetCars().Count == 0 && original.Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");
                else if ( (data.GetCars().Count - original.Count) == 0)
                    throw new SWWException("There are no new cars created!");

                Console.Clear();
                data.Save();
                logger.Info($"You have successfully saved {data.GetCars().Count - original.Count} cars to your simulation.");
                Console.WriteLine($"{data.GetCars().Count} - {original.Count}");

                original.Clear();
                foreach (Car car in data.GetCars())
                    original.Add(car);

                return true;
            }
            else if (option == ":reload")
            {
                if (data.EqualInstances(original))
                    throw new SWWException("You can't reload your database!");

                Console.Clear();
                data.GetCars().Clear();

                foreach (Car car in original)
                    data.AddCar(car);

                logger.Info("You have successfully reload your database.");

                return true;
            }
            else if (option == ":race")
            {
                if (data.GetCars().Count == 0)
                    throw new EmptyDatabaseException("There are no cars in the database!");

                Console.Clear();
                string index = SelectRace();

                Race race;
                if (index == "1") race = new IllegalRace();
                else if (index == "2") race = new Derby();
                else if (index == "3") race = new Drag();
                else
                    throw new UnknownKeyException($"There is no such option! ('{index}')");

                while (true)
                {
                    logger.Info($"You have started {GetRaceName(index)}.");

                    Console.WriteLine(
                              $"\n[DESCRIPTION]:\n" +
                              $" - Validity = {(race.Validity == null ? "N/A" : race.Validity)}\n" +
                              $" - Max participants = {(race.MaxParticipant == 0 ? "N/A" : race.MaxParticipant.ToString())}\n" +
                              $" - Minimum speed = {(race.MinimumSpeed == 0 ? "N/A" : race.MinimumSpeed.ToString() + "Km/h\n")}"
                              );

                    logger.Info($"You are already selected '{race.GetRaceCars().Count}'pcs of cars to the race.\n");

                    foreach (Car car in data.GetCars())
                    {
                        if (!car.Choosed)
                            Console.WriteLine(PrintCarProperties(car, false));
                    }

                    Console.WriteLine("\nPlease type a car license plate to choose it or write '0' to start the race.");
                    string plate = Console.ReadLine().ToUpper();

                    if (plate == "0")
                    {
                        if (race.GetRaceCars().Count <= 1)
                        {
                            Console.Clear();
                            logger.Error($"You haven't selected enough cars for the race yet! Choosen cars: {race.GetRaceCars().Count}");
                            continue;
                        }
                        else
                            break;
                    }

                    if (!Common.CheckPlateFormat(plate))
                    {
                        Console.Clear();
                        logger.Error($"Invalid license plate format! ('{plate}')");
                        continue;
                    }

                    if (!Common.CheckValidPlate(data.GetCars(), plate))
                    {
                        Console.Clear();
                        logger.Error($"Invalid license plate! ('{plate}')");
                        continue;
                    }

                    foreach (Car car in data.GetCars())
                    {
                        if (plate.Equals(car.LicensePlate))
                        {
                            if (!race.Contains(car))
                            {
                                race.AddCar(car);
                                car.Choosed = true;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                logger.Error("This car is already participate in the race!");
                            }
                        }
                    }
                }
                Console.Clear();
                logger.Info($"{GetRaceName(index)} result:");
                race.DoRace();

                Console.WriteLine($"\nParticipants: {race.GetRaceCars().Count}/{race.MaxParticipant}");
                foreach (Car car in race.GetRaceCars())
                {
                    Console.WriteLine(PrintCarProperties(car, false));
                }
                Console.WriteLine($"\nThe winner is {race.GetWinner().ToString()}!");

                return true;
            }
            else
                throw new UnknownKeyException($"There is no such option! ('{option}')");
        }

        public static void ManageException(ConsoleLogger logger, string message)
        {
            Console.Clear();
            logger.Error(message);
        }

        public static string SelectRace()
        {
            Console.Clear();
            List<string> types = new List<string>() {
                                                    "Illegal race",
                                                    "Derby",
                                                    "Drag race"
                                                     };
            for (int i = 0; i < types.Count; i++)
                Console.WriteLine($"({i + 1}). - {types[i]}");

            Console.WriteLine("\nSelect a race type by it's index.:");
            string index = Console.ReadLine();

            if (!int.TryParse(index, out int x))
                throw new InvalidInputException($"The entered value is not a number! ('{index}')");

            Console.Clear();
            return index;
        }

        public static string GetRaceName(string index)
        {
            if (index == "1") return "Illegal race";
            else if (index == "2") return "Derby";
            else if (index == "3") return "Drag race";
            else throw new SWWException($"Invalid race ID! ('{index}')");
        }

        public static string PrintCarProperties(Car car, bool check)
        {
            string text;
            if (check)
                text = $"License plate: {car.LicensePlate}\n" +
                                  $"Brand: {car.Brand}\n" +
                                  $"Color: {car.Color}\n" +
                                  $"Max speed: {car.MaxSpeed}Km/h\n" +
                                  $"Validity: {(car.Validity == true ? "Valid" : "Invalid")}";
            else
            {
                text = $"{CorrectString(car.LicensePlate, 0)} | " +
                        $"{CorrectString(car.Brand, 12)} | " +
                        $"{CorrectString(car.Color, 6)} | " +
                        $"{CorrectString(car.MaxSpeed.ToString() + "Km/h", 0)} | " +
                        $"{CorrectString((car.Validity == true ? "Valid" : "Invalid"), 0)}";
            }

            return text;
        }

        public static string CorrectString(string element, int num)
        {
            num -= element.Length;
            for (int i = 0; i < num; i++)
                element += " ";
            return element;
        }
    }
}
