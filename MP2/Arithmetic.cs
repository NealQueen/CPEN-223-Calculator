//MP2 Calculator 
//This file contains the Arithmethic class.

//You should implement the requesed method.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MP2
{
    public class Arithmetic
    {
        /// <summary>
        /// Use this method as is.
        /// It is called by Main and is used to get an expression from console
        /// which is passed to the Calculate method.
        /// </summary>
        /// <returns>The formatted expression and the expression evaluation result</returns>
        public static string BasicArithmetic()
        {
            Console.WriteLine();
            Console.WriteLine("Basic arithmetic opertions with + - * / ^");
            Console.WriteLine("Enter an expression:");
            string expression = Console.ReadLine().Trim();

            return Calculate(expression);
        }

        /// <summary>
        /// Evaluates the arithmetic expression passed to it and 
        /// returns a nicly formatted expression (proper spaces etc) and 
        /// the result.
        /// The precedence of the operator is enforced only using parenthesis.
        /// </summary>
        /// <returns>
        /// Returns the string that contains the arithmetic expression and the result,
        /// or the requested error message. 
        /// If the expression is not valid, it returns "Invalid expression"
        /// </returns>
        /// <example>
        /// If the expression is "2.1 + 3" then the method returns "2.1 + 3 = 5.1".
        /// If the expression is "(2 + 3) * (2 ^ 5) it returns "( 2 + 3 ) * ( 2 ^ 5 ) = 160" 
        /// If the expression is "2 + ((3 * 2) * 5)" it returns "2 + ( ( 3 * 2 ) * 5 ) = 32" 
        /// Extra spaces are fine, so if the user enters "  2   ^ 3 " then 
        /// the method returns "2 ^ 3 = 8".
        /// If the user enters "4 5" or "4 +" or " (4 + 5" or "4 + 5 * 4)" i.e. any incorrect 
        /// or unbalanced expression, then the method returns "Invalid expression".
        /// </example>
        public static string Calculate(string expression)
        {
            double result = double.NaN, a = 0, b = 0;
            string op = "";

            switch (op)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":

                    if (b != 0) result = a / b;
                    break;


                default:
                    break;
            }


            if (double.IsNaN(result)) return "Invalid expression";
            return result.ToString();
        }


        public static string SYA(Stack expression)
        {
            

            Stack<int> syaStack = new Stack<int>();
            Queue<int> syaQueue = new Queue<int>();

            foreach (char item in expression)
            {
                if (Char.IsDigit(item)) ;
                if (item.Equals('+') || item.Equals('-') || item.Equals('*') || item.Equals('/')) ;
            }

            return null;
        }

        public static Stack Reader(string expression)
        {
            char[] stringArray;
            stringArray = expression.ToCharArray();
            StringBuilder doublebuilder = new StringBuilder();

            for (int count = 0; count < stringArray.Length; count++)
            {

            }
            return null;
        }

    }

      
}
