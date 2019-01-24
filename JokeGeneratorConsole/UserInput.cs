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

        public Tuple<string, string> CustomName()
        {
            string fullName = "";
            string[] splitName;

            Console.Write("\nPlease enter custom name. Press [enter] to use default name: ");
            fullName = Word();
            if (fullName == "")
            {
                return Tuple.Create("Chuck", "Norris");
            }
            else
            {
                if (fullName.Contains(" "))
                {
                    splitName = fullName.Split(new char[] { ' ' }, 2);
                    return Tuple.Create(splitName[0], splitName[1]);
                }
                else
                {
                    return Tuple.Create(fullName, "");
                }
            }

        }
    }
}