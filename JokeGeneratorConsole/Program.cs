using System;
using System.Net;
using Newtonsoft.Json;


namespace JokeGenerator
{
    class Program
    {
        // variables
        private static bool loopGame;
        static UserInput read = new UserInput();
        static LoadFromWeb load = new LoadFromWeb();
        static string categoryList;
        static int categoryMatch;
        static char inputChar;
        static string category;
        static string gender;
        static string inputString;
        static Tuple<string, string> name;
        const string ChuckNorrisCategoriesLink = "https://api.chucknorris.io/jokes/categories";
        const string NameGeneratorLink = "https://uinames.com/api/";

        private static void DefaultValues()
        {
            loopGame = true;
            inputChar = '\0';
            inputString = "";
            categoryMatch = 0;
            category = "";
            categoryList = "";
            gender = "";
            name = Tuple.Create("Chuck", "Norris");
        }

        static void Main(string[] args)
        {
            DefaultValues();
            while (loopGame == true)
            {
                StartBanner();
                StartCondition();
                ViewCategories();
                SelectCategory();
                NameGenerator();
                RepeatGame();
            }
            System.Environment.Exit(0);
        }

        private static void StartBanner()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("--- Welcome to the Chuck Norris Joke Generator! ---");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Press [s] to start.");
            Console.WriteLine("Press [esc] at anytime to exit.");
        }

        private static void Error()
        {
            Console.WriteLine("\nInvalid input. Please try again.");
        }

        private static void StartCondition()
        {
            bool repeat = true;
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar != 's')
                {
                    Error();
                }
                else
                {
                    repeat = false;
                }
            }
        }

        private static void ViewCategories()
        {
            bool repeat = true;

            Console.WriteLine("\nWould you like to view all possible categories?  (Y/N)");
            categoryList = load.Categories(ChuckNorrisCategoriesLink);
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar == 'n')
                {
                    repeat = false;
                }
                else if (inputChar == 'y')
                {
                    Console.WriteLine("\n\n" + categoryList);
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static void SelectCategory()
        {
            bool repeat = true;

            Console.WriteLine("\nWould you like to select a category?  (Y/N)");
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar == 'n')
                {
                    repeat = false;
                }
                else if (inputChar == 'y')
                {
                    CategoryCheck();
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static void CategoryCheck()
        {
            bool repeat = true;

            Console.WriteLine("\nPlease enter a category.\nPress [enter] to skip.");
            while (repeat == true)
            {
                inputString = read.Word();
                categoryMatch = Match();
                if (categoryMatch == 0)
                {
                    // load without category
                    repeat = false;
                }
                else if (categoryMatch == 1)
                {
                    // load with category
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static void NameGenerator()
        {
            bool repeat = true;

            Console.WriteLine("\nWould you like to use a random or specific name?");
            Console.WriteLine("Press [d] for default, [r] for random, or [s] for specific name.");
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar == 'd')
                {
                    repeat = false;
                }
                else if (inputChar == 's')
                {
                    name = read.CustomName();
                    repeat = false;
                }
                else if (inputChar == 'r')
                {
                    GenderSpecify();
                    name = load.Identity(NameGeneratorLink + gender);
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
            Console.WriteLine("\nNew Name: (" + name.Item1 + ") (" + name.Item2 + ")");
        }

        private static void GenderSpecify()
        {
            bool repeat = true;

            Console.WriteLine("\nWould you like to specify a gender?");
            Console.WriteLine("Press [m] for male, [f] for female, or [n] to not specify.");
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar == 'm')
                {
                    gender = "?gender=male";
                    repeat = false;
                }
                else if (inputChar == 'f')
                {
                    gender = "?gender=female";
                    repeat = false;
                }
                else if (inputChar == 'n')
                {
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static int Match()
        {
            inputString.ToLower();
            categoryList.ToLower();
            if (inputString == "")
            {
                Console.WriteLine("No category has been chosen.");
                return 0;
            }
            else if (categoryList.Contains("\"" + inputString + "\""))
            {
                Console.WriteLine("Category has been found.");
                category = inputString;
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private static void RepeatGame()
        {
            bool repeat = true;

            Console.WriteLine("\nWould you like to play again?  (Y/N)");
            while (repeat == true)
            {
                inputChar = read.Key();
                if (inputChar == 'n')
                {
                    loopGame = false;
                    repeat = false;
                }
                else if (inputChar == 'y')
                {
                    DefaultValues();
                    repeat = false;
                    Console.Clear();
                }
                else
                {
                    Error();
                }
            }
        }
    }
}