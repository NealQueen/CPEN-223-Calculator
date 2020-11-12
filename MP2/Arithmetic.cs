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
            // the number stack and clean expression were created outside of the try, in order to allow outputing without creating redundant variables.
            Stack<object> numberstack = new Stack<object>();
            string cleanExpression;


            /// A try was used to allow the Console outputting of invalid expression without the program closing, and to allow for error messeges from
            /// internal methods to indicate errors without closing the program.
            try
            {   
                //Creates a Shunting-yard expression queue and a clean verion of the expression.
                Queue<object> expressionQueue = SYA(expression, out cleanExpression);


                //Reads through the SYA Queue and calculates the output accordingly.
                while ( expressionQueue.Count > 0 )
                {
                    if ( expressionQueue.Peek().GetType().Equals(typeof(double)) ) numberstack.Push(expressionQueue.Dequeue());
                    else if ( expressionQueue.Peek().Equals('+') )
                    {
                        double result = Convert.ToDouble(numberstack.Pop()) + Convert.ToDouble(numberstack.Pop());
                        numberstack.Push(result);
                        expressionQueue.Dequeue();
                    }
                    else if ( expressionQueue.Peek().Equals('-') )
                    {
                        double temp = Convert.ToDouble(numberstack.Pop());
                        double result = Convert.ToDouble(numberstack.Pop()) - temp;
                        numberstack.Push(result);
                        expressionQueue.Dequeue();
                    }
                    else if ( expressionQueue.Peek().Equals('*') )
                    {
                        double result = Convert.ToDouble(numberstack.Pop()) * Convert.ToDouble(numberstack.Pop());
                        numberstack.Push(result);
                        expressionQueue.Dequeue();
                    }
                    else if ( expressionQueue.Peek().Equals('/') )
                    {
                        double temp = Convert.ToDouble(numberstack.Pop());
                        double result = Convert.ToDouble(numberstack.Pop()) / temp;
                        numberstack.Push(result);
                        expressionQueue.Dequeue();
                    }
                    else if ( expressionQueue.Peek().Equals('^') )
                    {
                        double temp = Convert.ToDouble(numberstack.Pop());
                        double result = Math.Pow(Convert.ToDouble(numberstack.Pop()),temp);
                        numberstack.Push(result);
                        expressionQueue.Dequeue();
                    }
                }


                //Checks if after calculation only one result remains. Outputs an error otherwise.
                if ( numberstack.Count != 1 ) throw new ArgumentException("An error in calculation occured.");
            }
            catch ( Exception )
            {
                //Console.WriteLine(e+ e.Message);
                return "Invalid expression";
            }

            return (cleanExpression+numberstack.Pop().ToString());
        }


        /// <summary>
        /// Shunting-Yard Algorithm. Converts a standard form mathematical expression to a shunting-Yard expression.
        /// </summary>
        /// <param name="expression">The given expression to be converted.</param>
        /// <param name="cleanExpression"> and output parameter that holds a cleany spaced out version of the input expression. </param>
        /// <returns> A Queue with the elements of the Shunting Yard expression. </returns>
        /// <exception cref="ArgumentException"> Thrown when there is an error in the ordering and or syntax of the input equation. </exception>
        public static Queue<object> SYA(string expression, out string cleanExpression)
        {   
            //Changes the expression into a char array, and creates queue with only non-space values in it.
            char[] arr = expression.ToCharArray();
            Queue<object> expressionQueue = new Queue<object>();

            for ( int index = 0 ; index < arr.Length ; index++ )
            {

                if ( Char.IsDigit(arr[index]) )
                {
                    expressionQueue.Enqueue(DigitReader(arr,ref index));
                }
                else expressionQueue.Enqueue(arr[index]);
            }


            //Creates the cleanExpression string output from the expression queue.
            StringBuilder cleanExp = new StringBuilder();
            foreach ( object item in expressionQueue ) cleanExp.Append(item.ToString() + " ");
            cleanExp.Append("= ");
            cleanExpression = cleanExp.ToString();


            //Sorts the expression queue into a Shunting-Yard expression queue.
            Queue<object> syaQueue = new Queue<object>();
            Stack<object> operatorStack = new Stack<object>();

            foreach ( object item in expressionQueue )
            {
                if ( !item.GetType().Equals(typeof(double)) && !item.Equals('(') && operatorStack.Count == 0 && syaQueue.Count == 0 ) throw new ArgumentException("First element Invalid.");
                if ( item.GetType().Equals(typeof(double)) ) syaQueue.Enqueue(item);
                else if ( item.Equals('(') ) operatorStack.Push(item);
                else if ( item.Equals('^') && syaQueue.Count != 0 )
                {
                    if ( !( operatorStack.Count == 0 ) && operatorStack.Peek().Equals('^') )
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);
                }
                else if ( ( item.Equals('*') || item.Equals('/') ) && syaQueue.Count != 0 )
                {
                    if ( !( operatorStack.Count == 0 ) && ( operatorStack.Peek().Equals('^') || operatorStack.Peek().Equals('*') || operatorStack.Peek().Equals('/') ) )
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);

                }
                else if ( ( item.Equals('+') || item.Equals('-') ) && syaQueue.Count != 0 )
                {
                    if ( !( operatorStack.Count == 0 ) && ( operatorStack.Peek().Equals('^') || operatorStack.Peek().Equals('*') ||
                        operatorStack.Peek().Equals('/') || operatorStack.Peek().Equals('+') || operatorStack.Peek().Equals('-') ) )
                    {
                        syaQueue.Enqueue(operatorStack.Pop());
                        operatorStack.Push(item);
                    }
                    else operatorStack.Push(item);

                }
                else if ( ( item.Equals(')') && operatorStack.Contains('(') ) && syaQueue.Count != 0 )
                {
                    while ( !operatorStack.Peek().Equals('(') ) syaQueue.Enqueue(operatorStack.Pop());
                    operatorStack.Pop();
                }
                else if ( item.Equals(')') && !operatorStack.Contains('(') ) throw new ArgumentException("uneven parentheses");
                else throw new ArgumentException("invalid element.");
            }


            //Checks for unclosed parentheses and moves all remaining operators to the syaQueue.
            if ( operatorStack.Contains('(') ) throw new ArgumentException("unclosed parentheses");
            while ( operatorStack.Count != 0 ) syaQueue.Enqueue(operatorStack.Pop());

            return syaQueue;
        }


        /// <summary>
        /// Mainly a helper method for SYA that reads numbers.
        /// </summary>
        /// <param name="inputArray"> The character array in which the method reads the number.</param>
        /// <param name="index"> The reference index from character array. </param>
        /// <returns> Returns the selected number embedded in the char array as a double.</returns>
        /// <example> When referenced to the index of the first char of "22.56" will return the double(22.56) </example>
        public static double DigitReader(Char[] inputArray,ref int index)
        {
            StringBuilder sb = new StringBuilder();
            while ( index != inputArray.Length && ( Char.IsDigit(inputArray[index]) || inputArray[index].Equals('.') ) )
            {
                sb.Append(inputArray[index]);
                index++;
            }
            index -= 1;
            return double.Parse(sb.ToString());
        }


    }

}
