using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace JokeGenerator
{
    public class UserInput
    {
        public static object Decipher;
        private static ConsoleKeyInfo character;
        static char readChar;

        public char Key()
        {
            readChar = Console.ReadKey().KeyChar;
            
            // escape button condition
            if (readChar == '\u001B')
            {
                System.Environment.Exit(1);
            }
            return readChar;
        }

    }
}