using System;

namespace JokeGenerator
{
    class Program
    {
        // variables
        private static bool loopGame;
        static UserInput read = new UserInput();
        static char inputChar;

        private static void DefaultValues()
        {
            loopGame = true;
        }

        static void Main(string[] args)
        {
            DefaultValues();
            while (loopGame == true)
            {
                StartBanner();
                StartCondition();
                RepeatGame();
            }
            
            System.Environment.Exit(1);
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
                    Console.WriteLine("\nGameStart.");
                    repeat = false;
                }
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