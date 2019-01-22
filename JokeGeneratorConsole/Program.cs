using System;

namespace JokeGenerator
{
    class Program
    {
        static UserInput read = new UserInput();
        static void Main(string[] args)
        {
            char inputChar;
            StartBanner();
            inputChar = read.Key();
            Console.WriteLine(inputChar);
            // currently line underneath is used to read previous commands
            inputChar = read.Key();
            // System.Environment.Exit(1);
        }

        private static void StartBanner()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("--- Welcome to the Chuck Norris Joke Generator! ---");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Press [s] to start.");
            Console.WriteLine("Press [esc] at anytime to exit.");
        }

        private static void UserInput(ConsoleKeyInfo KeyInfo)
        {
        }
    }
}