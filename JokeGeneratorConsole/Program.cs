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
        static string inputString;
        static string category;
        static string gender;
        static int numJokes;
        static string actualJoke;
        static Tuple<string, string> name;
        const string ChuckNorrisCategoriesLink = "https://api.chucknorris.io/jokes/categories";
        const string ChuckNorrisJokeLink = "https://api.chucknorris.io/jokes/random?";
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
            numJokes = 0;
            actualJoke = "";
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
                JokeAmount();
                DisplayJokes();
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
                if (categoryMatch == 0 || categoryMatch == 1)
                {
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

        private static void JokeAmount()
        {
            bool repeat = true;

            Console.WriteLine("\nPlease input the amount of jokes you wish to view.  (1-9)");
            while (repeat == true)
            {
                inputChar = read.Key();
                if (Char.IsDigit(inputChar) && inputChar != '0')
                {
                    numJokes = (int) Char.GetNumericValue(inputChar);
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static void DisplayJokes()
        {
            Console.WriteLine("\n\nJokes:");
            for (int i = 1; i <= numJokes; i++)
            {
                GetJoke();
                NameSwap();
                Console.WriteLine(i + ". " + actualJoke);
            }
        }

        private static void GetJoke()
        {
            if (categoryMatch == 0)
            {
                actualJoke = load.Joke(ChuckNorrisJokeLink);
            }
            else
            {
                actualJoke = load.Joke(ChuckNorrisJokeLink + "category=" + category);
            }
        }

        private static void NameSwap()
        {
            actualJoke = actualJoke.Replace("Chuck", (name.Item1), StringComparison.OrdinalIgnoreCase);
            actualJoke = actualJoke.Replace("Norris", (name.Item2), StringComparison.OrdinalIgnoreCase);
            if (gender == "?gender=female")
            {
                actualJoke = actualJoke.Replace(" he ", " she ", StringComparison.OrdinalIgnoreCase);
                actualJoke = actualJoke.Replace(" his ", " her ", StringComparison.OrdinalIgnoreCase);
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