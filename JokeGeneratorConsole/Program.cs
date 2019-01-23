using System;

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
        static string inputString;
        const string ChuckNorrisCategoriesLink = "https://api.chucknorris.io/jokes/categories";

        private static void DefaultValues()
        {
            loopGame = true;
            inputChar = '\0';
            inputString = "";
            categoryMatch = 0;
            categoryList = "";
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
                categoryMatch = match();
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

        private static int match()
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