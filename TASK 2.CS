using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter a mathematical phrase ( 5 + 5 , 6 * 7 , ... ): ");
            String pattern = @"^(\d(?:\d+)?)(?:[-+*/]\d+(?:\d+)?)$";
            string input = Console.ReadLine();
            Regex reg = new Regex(pattern);
            if (!reg.IsMatch(input))
            {
                Console.WriteLine("Error: Invalid phrase ");
                Console.ReadLine();
                Environment.Exit(0);
            }
            string[] phrase = Regex.Split(input, "[-+*/]");
            double num1 = Int32.Parse(phrase[0]);
            double num2 = Int32.Parse(phrase[1]);
            char op = Regex.Match(input, "[-+*/]").Value[0];
            double result = 0.0;
            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error: Cannot divide by zero");
                        Console.ReadLine();
                        return;
                    }
                    result = num1 / num2;
                    break;
                default:
                    break;
            }
            Console.WriteLine("Result: " + result);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}

}
