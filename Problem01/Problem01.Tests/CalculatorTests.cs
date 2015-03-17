using NUnit.Framework;
using System;

namespace Problem01.Tests
{
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void WhenCallingAddMethodWithEmptyString_ShouldReturnZero()
        {
            var result = calculator.Add(String.Empty);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenCallingAddMethodWithOneNumber_ShouldReturnThatNumber()
        {
            var result = calculator.Add("2");

            Assert.AreEqual(2, result);
        }

        [Test]
        public void WhenCallingAddMethodWithNull_ShouldRetrunZero()
        {
            var result = calculator.Add(null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenCallingAddMethodWithTwoNumbers_ShouldReturnNumbersSum()
        {
            var result = calculator.Add("2,3");

            Assert.AreEqual(5, result);
        }

        [Test]
        public void WhenCallingAddMethodWithManyNumbers_ShouldReturnNumbersSum()
        {
            var result = calculator.Add("1,1,2,3,5");

            Assert.AreEqual(12, result);
        }

        [Test]
        public void WhenCallingAddMethodWithLetter_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("d")); 
            Assert.AreEqual("Some input values are not numbers: d.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodWithEmptyInside_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("2,"));
            Assert.AreEqual("Some input values are not numbers: empty.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodWithNumbersAndLetters_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("d,4,sdafa,6,2, ,9,,1"));
            Assert.AreEqual("Some input values are not numbers: d,sdafa,empty,empty.", ex.Message);
        }

        [Test]
        public void WhenCallingAddWithNewLineAsSeparator_ShouldCalculateProperSum()
        {
            var result = calculator.Add("1\n2");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenCallingAddWithDifferentSeparators_ShouldCalculateProperSum()
        {
            var result = calculator.Add("1\n2,3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void WhenCallingAddMethodWithDifferentSeperatorsAndEmpty_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("1,\n"));
            Assert.AreEqual("Some input values are not numbers: empty,empty.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodWithCustomSeparator_ShouldCalculateProperSum()
        {
            var result = calculator.Add("//;\n1;2");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenCallingAddMethodOnlyWithCustomSeparator_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("//;\n"));
            Assert.AreEqual("Some input values are not numbers: empty.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodOnlyWithCustomSeparatorBeginning_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("//"));
            Assert.AreEqual("No separator provided.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodWithNegativeNumbers_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add("-1,,9,-2"));
            Assert.AreEqual("Negatives not allowed: -1,-2.", ex.Message);
        }

        [Test]
        public void WhenCallingAddMethodWithNumbersBiggerThan1000_ShouldIgnoreBigNumbers()
        {
            var result = calculator.Add("3,1001,5,5927329,1000");
            Assert.AreEqual(1008, result);
        }

        [Test]
        public void WhenCallingAddMethodWithLongCustomDelimiter_ShouldReturnProperSum()
        {
            var result = calculator.Add("//[***]\n1***2***3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void WhenCallingAddMethodWithManyCustomDelimiters_ShouldReturnProperSum()
        {
            var result = calculator.Add("//[*****][%]\n1*****2%3");
            Assert.AreEqual(6, result);
        }
    }
}
