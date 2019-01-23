using System;

namespace JokeGenerator
{
    public class UserInput
    {
        public static object Decipher;
        //private static ConsoleKeyInfo character;
        static char readChar;
        static string readString;

        public char Key()
        {
            readChar = Console.ReadKey().KeyChar;
            if (readChar == '\u001B') // esc key
            {
               System.Environment.Exit(0);
            }
            return readChar;
        }

        public string Word()
        {
            readString = Console.ReadLine();
            return readString;
        }

        
    }
}