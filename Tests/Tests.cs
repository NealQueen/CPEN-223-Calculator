using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP2;
using System;
using System.IO;

namespace Tests
{
    /// <remarks>
    /// Throwing exceptions was not tested in some cases as it is handled by the calculator main class and 
    /// can be easily and repeatedly human tested for different cases.
    /// </remarks
    [TestClass()]
    public class Mp2_Tests
    {
        
        [TestMethod()]
        public void Arithmetic1()
        {
            string result = Arithmetic.Calculate("2+3*5-6+4");
            Assert.AreEqual("2 + 3 * 5 - 6 + 4 = 15",result);
        }

        [TestMethod()]
        public void Arithmetic2()
        {
            string result = Arithmetic.Calculate("1.1 + 2");
            Assert.AreEqual("1.1 + 2 = 3.1",result);
        }

        [TestMethod()]
        public void Arithmetic3()
        {
            string result = Arithmetic.Calculate("1 + -2");
            Assert.AreEqual("1 + -2 = -1",result);
        }

        [TestMethod()]
        public void Arithmetic4()
        {
            string result = Arithmetic.Calculate("(1 + 2) * 3");
            Assert.AreEqual("( 1 + 2 ) * 3 = 9",result);
        }

        [TestMethod()]
        public void Arithmetic5()
        {
            string result = Arithmetic.Calculate("(((1 + 3) ^ 2) + 1)* 3");
            Assert.AreEqual("( ( ( 1 + 3 ) ^ 2 ) + 1 ) * 3 = 51",result);
        }

        [TestMethod()]
        public void Calc_Setpoly_Error() 
        {
            var sr = new StringReader("error.");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(false,result); 
        }

        [TestMethod()]
        public void Calc_Setpoly1()
        {
            var sr = new StringReader("2 0 3");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void Calc_Setpoly2()
        {
            var sr = new StringReader("-0.5 3.666 558");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void Calc_EvalPoly1()
        {
            var sr = new StringReader("2 5 9");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomial(2);
            Assert.AreEqual(27,result);
        }

        [TestMethod()]
        public void Calc_EvalPoly2()
        {
            var sr = new StringReader("-2. 6.5 5.5 -17.5 2 5 1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomial(2);
            Assert.AreEqual(47,result);
        }

        [TestMethod()]
        public void Calc_EvalPoly3()
        {
            var sr = new StringReader("-222 225 59");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomial(2);
            Assert.AreEqual(-379,result);
        }

        [TestMethod()]
        public void Calc_EvalDer1()
        {
            var sr = new StringReader("2 0 1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialDerivative(2);
            Assert.AreEqual(8,result);
        }

        [TestMethod()]
        public void Calc_EvalDer2()
        {
            var sr = new StringReader("1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialDerivative(2);
            Assert.AreEqual(0,result);
        }

        [TestMethod()]
        public void Calc_EvalDer3()
        {
            var sr = new StringReader("2 2 0 -1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialDerivative(2);
            Assert.AreEqual(32,result);
        }

        [TestMethod()]
        public void Calc_EvalDer4()
        {
            var sr = new StringReader("1 -0.56 25 35 -7.25 1.5 0.005               000");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialDerivative(2);
            Assert.AreEqual(3379.485,result);
        }

        [TestMethod()]
        public void Calc_EvalInte1()
        {
            var sr = new StringReader("1 -0.56 25 35 -7.25 1.5 0.005               000");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialIntegral(-1,1);
            Assert.AreEqual(14.839,result,0.001);
        }

        [TestMethod()]
        public void Calc_EvalInte2()
        {
            var sr = new StringReader("2 0 1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialIntegral(2,5);
            Assert.AreEqual(81,result);
        }

        [TestMethod()]
        public void Calc_EvalInte3()
        {
            var sr = new StringReader("0");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialIntegral(2,5);
            Assert.AreEqual(0,result);
        }

        [TestMethod()]
        public void Calc_EvalInte4()
        {
            var sr = new StringReader("2 0 1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialIntegral(0,0);
            Assert.AreEqual(0,result);
        }

        [TestMethod()]
        public void Calc_EvalInte5()
        {
            var sr = new StringReader("2 0 1");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomialIntegral(-1,0);
            Assert.AreEqual(1.666,result,0.01);
        }

    }
}
