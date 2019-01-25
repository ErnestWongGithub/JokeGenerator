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
            ConsoleSettings();
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

        private static void ConsoleSettings()
        {
            int width = 91;
            int height = 37;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        private static void StartBanner()
        {
            string title = "Welcome to the Chuck Norris Joke Generator!";
            string spacing = new String (' ', 22);
            string borderline = new String ('═', 43);
            Console.WriteLine(" ╔{0}═{0}╗", borderline);
            Console.WriteLine(" ║{0}{1}{0}║", spacing, title);
            Console.WriteLine(" ╚{0}═{0}╝", borderline);
        }

        private static void Error()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        private static void StartCondition()
        {
            bool repeat = true;

            while (repeat == true)
            {
                Console.Write("Press [s] to start or [esc] to exit anytime. ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) != 's')
                {
                    Error();
                }
                else
                {
                    repeat = false;
                }
            }
            Console.WriteLine();
        }

        private static void ViewCategories()
        {
            bool repeat = true;
            string[] separateCategory;

            categoryList = load.Categories(ChuckNorrisCategoriesLink);
            categoryList = categoryList.Replace("\"", "");
            categoryList = categoryList.Replace(",", "] [");
            separateCategory = categoryList.Split(" ");
            while (repeat == true)
            {
                Console.Write("Would you like to view all possible categories?  (Y/N): ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) == 'n')
                {
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'y')
                {
                    Console.WriteLine("\n\nCategories:");
                    if (separateCategory.Length != 1)
                    {
                        Console.WriteLine(separateCategory[0] + separateCategory[1] + separateCategory[2] + separateCategory[3] + separateCategory[4] + separateCategory[5] + separateCategory[6] + separateCategory[7]);
                        Console.WriteLine(separateCategory[8] + separateCategory[9] + separateCategory[10] + separateCategory[11] + separateCategory[12] + separateCategory[13] + separateCategory[14] + separateCategory[15]);
                    }
                    else
                    {
                        Console.WriteLine("Database could not be loaded.");
                    }
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
            Console.WriteLine();
        }

        private static void SelectCategory()
        {
            bool repeat = true;

            while (repeat == true)
            {
                Console.Write("Would you like to select a category?  (Y/N): ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) == 'n')
                {
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'y')
                {
                    Console.WriteLine();
                    CategoryCheck();
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
            Console.WriteLine();
        }

        private static void CategoryCheck()
        {
            bool repeat = true;

            while (repeat == true)
            {
                Console.Write("Please enter a category. Press [enter] to skip: ");
                inputString = read.Word();
                categoryMatch = Match();
                if (categoryMatch == 0 || categoryMatch == 1)
                {
                    repeat = false;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Error();
                }
            }
        }

        private static void NameGenerator()
        {
            bool repeat = true;

            Console.WriteLine("Would you like to use a random or specific name?");
            while (repeat == true)
            {
                Console.Write("Press [d] for default, [r] for random, or [s] for specific name: ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) == 'd')
                {
                    Console.WriteLine();
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 's')
                {
                    name = read.CustomName();
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'r')
                {
                    Console.WriteLine();
                    GenderSpecify();
                    name = load.Identity(NameGeneratorLink + gender);
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
        }

        private static void GenderSpecify()
        {
            bool repeat = true;

            Console.WriteLine("Would you like to specify a gender?");
            while (repeat == true)
            {
                Console.Write("Press [m] for male, [f] for female, or [n] to not specify: ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) == 'm')
                {
                    gender = "?gender=male";
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'f')
                {
                    gender = "?gender=female";
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'n')
                {
                    repeat = false;
                }
                else
                {
                    Error();
                }
            }
            Console.WriteLine();
        }

        private static void JokeAmount()
        {
            bool repeat = true;

            while (repeat == true)
            {
                Console.Write("Please input the amount of jokes you wish to view.  (1-9): ");
                inputChar = read.Key();
                if (Char.IsDigit(inputChar) && inputChar != '0')
                {
                    numJokes = (int)Char.GetNumericValue(inputChar);
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
            if (name.Item2 != "")
            {
                actualJoke = actualJoke.Replace("Chuck", (name.Item1), StringComparison.OrdinalIgnoreCase);
                actualJoke = actualJoke.Replace("Norris", (name.Item2), StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                actualJoke = actualJoke.Replace("Chuck Norris", (name.Item1), StringComparison.OrdinalIgnoreCase);
            }
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
                return 0;
            }
            else if (categoryList.Contains("[" + inputString + "]"))
            {
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

            Console.WriteLine();
            while (repeat == true)
            {
                Console.Write("Would you like to play again?  (Y/N): ");
                inputChar = read.Key();
                if (Char.ToLower(inputChar) == 'n')
                {
                    loopGame = false;
                    repeat = false;
                }
                else if (Char.ToLower(inputChar) == 'y')
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