using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorTestTask;

namespace ClaculatorTester
{
    [TestClass]
    public class CalculatorTester
    {
        [TestMethod]
        public void TestValidityChecks()
        {
            var expression1 = "10+";
            var expression2 = "x+";
            var expression3 = "10_";
            var expression4 = "(10+25";
            var expression5 = "10.2+12.4";
            var expression6 = "10,2,4 + 12,4";
            var expression7 = "10,2+12,2/(24+12)";


            var result1 = CalculatorTestTask.Calculator.CheckValidity(expression1);
            var result2 = CalculatorTestTask.Calculator.CheckValidity(expression2);
            var result3 = CalculatorTestTask.Calculator.CheckValidity(expression3);
            var result4 = CalculatorTestTask.Calculator.CheckValidity(expression4);
            var result5 = CalculatorTestTask.Calculator.CheckValidity(expression5);
            var result6 = CalculatorTestTask.Calculator.CheckValidity(expression6);
            var result7 = CalculatorTestTask.Calculator.CheckValidity(expression7);




            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(false, result5);
            Assert.AreEqual(false, result6);
            Assert.AreEqual(true, result7);
        }

        [TestMethod]
        public void TestCorrectActionOrder()
        {
            var expression1 = "10+20/2";
            var expression2 = "10+-20+15";
            var expression3 = "10*20+-45/5";
            var expression4 = "10*2/20";

            var result1 = CalculatorTestTask.Calculator.CalculateExpression(expression1);
            var result2 = CalculatorTestTask.Calculator.CalculateExpression(expression2);
            var result3 = CalculatorTestTask.Calculator.CalculateExpression(expression3);
            var result4 = CalculatorTestTask.Calculator.CalculateExpression(expression4);

            Assert.AreEqual("20", result1);
            Assert.AreEqual("5", result2);
            Assert.AreEqual("191", result3);
            Assert.AreEqual("1", result4);
        }

        [TestMethod]
        public void TestCorrectActionOrderWithBrackets()
        {
            var expression1 = "(10+20)/2";
            var expression2 = "(10+-20)+15*(10/2)";
            var expression3 = "((10+20)*-45)/5";
            var expression4 = "(((10+5)/5)+-20)*5";
            var expression5 = "((((10+5+5)/5)+-20)*5+(10/2))";

            var result1 = CalculatorTestTask.Calculator.CalculateExpression(expression1);
            var result2 = CalculatorTestTask.Calculator.CalculateExpression(expression2);
            var result3 = CalculatorTestTask.Calculator.CalculateExpression(expression3);
            var result4 = CalculatorTestTask.Calculator.CalculateExpression(expression4);
            var result5 = CalculatorTestTask.Calculator.CalculateExpression(expression5);

            Assert.AreEqual("15", result1);
            Assert.AreEqual("65", result2);
            Assert.AreEqual("-270", result3);
            Assert.AreEqual("-85", result4);
        }
    }
}
