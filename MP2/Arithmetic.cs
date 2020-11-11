//MP2 Calculator 
//This file contains the Arithmethic class.

//You should implement the requesed method.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            return null;
        }

        public static Queue<object> SYA(string expression)
        {
            //Creates a queue with functioning numbers
            char[] arr = expression.ToCharArray();
            Queue<object> expressionQueue = new Queue<object>();

            for (int index = 0; index < arr.Length; index++)
            {

                if (Char.IsDigit(arr[index]))
                {
                    expressionQueue.Enqueue(DigitReader(arr, ref index));
                }
                else expressionQueue.Enqueue(arr[index]);
            }


            //sorts the queue into a syaQueue
            Queue<object> syaQueue = new Queue<object>();
            Stack<object> operatorStack = new Stack<object>();

            foreach (object item in expressionQueue)
            {
                if (item.GetType().Equals(typeof(double))) syaQueue.Enqueue(item);
                else if (item.Equals('(')) operatorStack.Push(item);
                else if (item.Equals('^'))
                {
                    if (operatorStack.Peek().Equals('^'))
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);
                }
                else if (item.Equals('*') || item.Equals('/'))
                {
                    if (operatorStack.Peek().Equals('^') || operatorStack.Peek().Equals('*') || operatorStack.Peek().Equals('/'))
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);

                }
                else if (item.Equals('+') || item.Equals('-'))
                {
                    if (operatorStack.Peek().Equals('^') || operatorStack.Peek().Equals('*') || operatorStack.Peek().Equals('/') || operatorStack.Peek().Equals('+') || operatorStack.Peek().Equals('-'))
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);

                }
                else if (item.Equals(')') && operatorStack.Contains('('))
                {
                    while (!operatorStack.Peek().Equals('(')) syaQueue.Enqueue(operatorStack.Pop());
                    operatorStack.Pop();
                }
                else if (item.Equals(')') && !operatorStack.Contains('(')) throw new ArgumentException("uneven parentheses");
            }

            if (operatorStack.Contains('(')) throw new ArgumentException("unclosed parentheses");
            while (operatorStack.Count!=0) syaQueue.Enqueue(operatorStack.Pop());
            

        }

        public static double DigitReader(Char[] inputArray, ref int index)
        {
            StringBuilder sb = new StringBuilder();
            while (index != inputArray.Length && (Char.IsDigit(inputArray[index]) || inputArray[index].Equals('.')))
            {
                sb.Append(inputArray[index]);
                index++;
            }
            index -= 1;
            return double.Parse(sb.ToString());
        }

    }

      
}
