using System;
using NUnit.Framework;

namespace TestingTechniques.UnitTests
{
    [TestFixture]
    public class TransportSubscriptionCardPriceCalculatorTests
    {
        private const string GreaterThanZeroExpectionMessage = "The age should be greater than zero.";
        private const string SmallerThan123ExpectionMessage = "The age should be smaller than 123.";
        private const string ShouldBeIntegerExpectionMessage = "The age input should be an integer value between 0 - 122.";

        ////[TestCase("-1", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = GreaterThanZeroExpectionMessage)]
        ////[TestCase("0", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = GreaterThanZeroExpectionMessage)]
        [TestCase("1", 0)]
        [TestCase("4", 0)]
        [TestCase("5", 0)]
        [TestCase("6", 20)]
        [TestCase("17", 20)]
        [TestCase("18", 20)]
        [TestCase("19", 40)]
        [TestCase("64", 40)]
        [TestCase("65", 5)]
        [TestCase("66", 5)]
        [TestCase("121", 5)]
        [TestCase("122", 5)]
        //////[TestCase("123", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = SmallerThan123ExpectionMessage)]
        //////[TestCase("a", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        //////[TestCase("", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        //////[TestCase(null, 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        //////[TestCase("2147483648", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        //////[TestCase("–2147483649", 0, ExpectedException = typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        public void ValidateCalculateSubscriptionPrice1(string ageInput, decimal expectedPrice)
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice(ageInput);

            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [Test]
        public void ValidateCalculateSubscriptionPrice_Free([Random(min: 1, max: 5, count: 1)]
                                                            int ageInput)
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice(ageInput.ToString());

            Assert.AreEqual(0, actualPrice);
        }

        [Test]
        public void ValidateCalculateSubscriptionPrice_20lv([Random(min: 6, max: 18, count: 1)]
                                                            int ageInput)
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice(ageInput.ToString());

            Assert.AreEqual(20, actualPrice);
        }

        [Test]
        public void ValidateCalculateSubscriptionPrice_40lv([Random(min: 19, max: 64, count: 1)]
                                                            int ageInput)
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice(ageInput.ToString());

            Assert.AreEqual(40, actualPrice);
        }

        [Test]
        public void ValidateCalculateSubscriptionPrice_5lv([Random(min: 65, max: 122, count: 1)]
                                                           int ageInput)
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice(ageInput.ToString());

            Assert.AreEqual(5, actualPrice);
        }

        [Test]
        ////[ExpectedException(typeof(ArgumentException), ExpectedMessage = ShouldBeIntegerExpectionMessage)]
        public void ValidateCalculateSubscriptionPrice_NotInteger()
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice("invalid");

            Assert.AreEqual(5, actualPrice);
        }

        [Test]
        ////[ExpectedException(typeof(ArgumentException), ExpectedMessage = GreaterThanZeroExpectionMessage)]
        public void ValidateCalculateSubscriptionPrice_InvalidZero()
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice("0");

            Assert.AreEqual(5, actualPrice);
        }

        [Test]
        ////[ExpectedException(typeof(ArgumentException), ExpectedMessage = SmallerThan123ExpectionMessage)]
        public void ValidateCalculateSubscriptionPrice_InvalidGreater122()
        {
            decimal actualPrice = TransportSubscriptionCardPriceCalculator.CalculateSubscriptionPrice("1000");

            Assert.AreEqual(5, actualPrice);
        }
    }
}