using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP2;
using System;
using System.IO;

namespace Tests
{
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
        public void Calculus1() 
        {
            var sr = new StringReader("error.");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(false,result); 
        }

        [TestMethod()]
        public void Calculus2()
        {
            var sr = new StringReader("2 0 3");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void Calculus3()
        {
            var sr = new StringReader("-0.5 3.666 558");
            Console.SetIn(sr);
            var calculus = new Calculus();

            var result = calculus.SetPolynomial();
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void Calculus4()
        {
            var sr = new StringReader("2 5 9");
            Console.SetIn(sr);
            var calculus = new Calculus();

            calculus.SetPolynomial();
            var result = calculus.EvaluatePolynomial(2);
            Assert.AreEqual(27,result);
        }
    }
}
