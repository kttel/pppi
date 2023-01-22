using System;
using System.IO;

namespace lab1
{
    internal class Program
    {
        private static string[] loremIpsum;
        static void Main(string[] args)
        {
            Option[] menu = {
                new Option(1, "Get Lorem Ipsum by words amount"),
                new Option(2, "Calculate some values"),
                new Option(3, "Exit")
            };

            bool is_alive = true;
            while (is_alive)
            {
                Console.WriteLine("Select the option of menu:");
                foreach (Option option in menu)
                {
                    Console.WriteLine($"\t{option.id}. {option.description}.");
                }

                Console.Write("Your option: ");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect value! Try again.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        GetLoremIpsum();
                        continue;
                    case 2:
                        Calculate();
                        continue;
                    case 3:
                        is_alive = false;
                        break;
                    default:
                        Console.WriteLine("Invalid value! Try again");
                        continue;
                }
            }
        }
        static void GetLoremIpsum()
        {
            string filePath = "D:\\lorem-ipsum.txt";
            loremIpsum = File.ReadAllText(filePath).Split(' ');

            Console.Write("Enter the number of LI words: ");
            try
            {
                int amountOfWords = int.Parse(Console.ReadLine());
                for (int word = 0; word < amountOfWords; word++)
                {
                    Console.Write($"{loremIpsum[word]} ");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\n\nEnd of the text! Returning to the menu...");
            }
            catch (Exception)
            {
                Console.WriteLine("\nError occured! Returning to the menu...");
            }

            Console.WriteLine("\n");
        }

        static void Calculate()
        {
            double result;
            Option[] operations = {
                new Option(1, "Add"),
                new Option(2, "Substract"),
                new Option(3, "Multiply"),
                new Option(4, "Divide")
            };

            Console.WriteLine("Operations:");
            foreach (Option option in operations)
            {
                Console.WriteLine($"\t{option.id}. {option.description}.");
            }
            Console.Write("Select an operation: ");
            int operation = int.Parse(Console.ReadLine());

            Console.Write("Enter two numbers separated by space: ");
            string[] numbers = Console.ReadLine().Split(' ');

            if (numbers.Length != 2)
            {
                Console.WriteLine("Wrong numbers! Returning to the menu...\n\n");
                return;
            }
            try
            {
                switch (operation)
                {
                    case 1:
                        result = int.Parse(numbers[0]) + int.Parse(numbers[1]);
                        break;
                    case 2:
                        result = int.Parse(numbers[0]) - int.Parse(numbers[1]);
                        break;
                    case 3:
                        result = int.Parse(numbers[0]) * int.Parse(numbers[1]);
                        break;
                    case 4:
                        result = int.Parse(numbers[0]) / int.Parse(numbers[1]);
                        break;
                    default:
                        Console.WriteLine("Calculation error! Returning to the menu...\n\n");
                        return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid values! Returning to the menu...\n\n");
                return;
            }

            Console.WriteLine($"Your result: {result}\n");
        }
    }
    class Option
    {
        public int id;
        public string description;
        public Option(int id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}