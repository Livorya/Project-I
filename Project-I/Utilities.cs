
namespace Project_I
{
    internal static class Utilities
    {
        public static void ResetTextColor()
        {   //Method that reset the text color to white
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteArrows()
        {   //Method that writes cyan arrows
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            ResetTextColor();
        }
        public static void WriteArrowedText(string text, bool newLine = false)
        {   //Method that writes cyan arrows followed by given text and gives the option to end with a new line
            WriteArrows();
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }
        public static void WriteArrowedText(string text, ConsoleColor color, bool newLine = false)
        {   //Method that writes cyan arrows followed by given text in a given color
            //and gives the option to end with a new line
            WriteArrows();
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
            ResetTextColor();
        }
        public static void WriteLineArrowedColoredNumberText(int number, ConsoleColor color, string text)
        {   //Method that writes cyan arrows followed by a number in a given color followed by a given text
            WriteArrows();
            WriteColoredNumber(number, color);
            Console.WriteLine(" " + text);
        }
        public static void WriteColoredNumber(int number, ConsoleColor color)
        {   //Method that takes a number and a color and then writes only the number colored inside parantheses
            Console.Write("(");
            Console.ForegroundColor = color;
            Console.Write(number);
            ResetTextColor();
            Console.Write(")");
        }
        public static void WriteColoredText(string text, ConsoleColor color, bool newLine = false)
        {   //Method that writes given text in a gives color and then gives the option to end with a new line
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
            ResetTextColor();
        }
        public static void WritePickOption()
        {   //Method that writes cyan arrows followed by a pick option message
            WriteArrows();
            Console.Write("Pick a Option: ");
        }
        public static void WriteLineColoredTitle(string text, ConsoleColor color)
        {   //Method that writes a given title in a given color
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine("   ** " + text + " **");
            ResetTextColor();
        }
    }
}
