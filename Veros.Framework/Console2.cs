namespace Veros.Framework
{
    using System;

    public class Console2
    {
        public static void Alert(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public static void Error(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public static void Normal(string message, params object[] args)
        {
            Normal(message, true, args);
        }

        public static void NormalNoNewLine(string message, params object[] args)
        {
            Normal(message, false, args);
        }

        public static void Success(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
        
        private static void Normal(string message, bool newLine, params object[] args)
        {
            if (newLine)
            {
                Console.WriteLine(message, args);
            }
            else
            {
                Console.Write(message, args);
            }
        }
    }
}