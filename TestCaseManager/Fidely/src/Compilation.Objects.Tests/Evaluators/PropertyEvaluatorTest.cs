using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;
using Fidely.Framework.Compilation.Objects.Evaluators;

namespace Fidely.Framework.Compilation.Objects.Tests.Evaluators
{
    [TestFixture]
    public class PropertyEvaluatorTest
    {
        private PropertyEvaluator<Entity> testee;


        [SetUp]
        public void SetUp()
        {
            testee = new PropertyEvaluator<Entity>();
        }


        [Test]
        public void RecognizableOperandsShouldReturnCorrectValues()
        {
            var expected = new string[] {
                "Id:",
                "Name:",
                "n:Item Name",
                "UnitPrice:Unit Price",
                "p:Unit Price",
                "price:Unit Price",
                "DiscountRate:",
                "rate:"
            };
            CollectionAssert.AreEquivalent(expected, testee.AutocompleteItems.Select(o => o.DisplayName + ":" + o.Description).ToList());
        }

        [Test]
        public void EvaluateShouldReturnNullWhenPassedExpressionIsNull()
        {
            Assert.IsNull(testee.Evaluate(null, ""));
        }

        [Test]
        public void EvaluateShouldReturnNullWhenPassedValueIsNull()
        {
            Assert.IsNull(testee.Evaluate(Expression.Constant(""), null));
        }

        [TestCase("XXX")]
        [TestCase("Description")]
        public void EvaluateShouldReturnNullWhenNoRegisteredPropertyIsPassed(string propertyName)
        {
            var entity = new Entity { Id = 1, Name = "Pen", UnitPrice = 10.25m, DiscountRate = 0.5, Description = "Blank Pen" };
            Assert.IsNull(testee.Evaluate(Expression.Constant(entity), propertyName));
        }

        [TestCase("id", Result = "System.Decimal:1")]
        [TestCase("Id", Result = "System.Decimal:1")]
        [TestCase("ID", Result = "System.Decimal:1")]
        [TestCase("name", Result = "System.String:Pen")]
        [TestCase("Name", Result = "System.String:Pen")]
        [TestCase("NAME", Result = "System.String:Pen")]
        [TestCase("n", Result = "System.String:Pen")]
        [TestCase("N", Result = "System.String:Pen")]
        [TestCase("unitprice", Result = "System.Decimal:10.25")]
        [TestCase("UnitPrice", Result = "System.Decimal:10.25")]
        [TestCase("UNITPRICE", Result = "System.Decimal:10.25")]
        [TestCase("p", Result = "System.Decimal:10.25")]
        [TestCase("P", Result = "System.Decimal:10.25")]
        [TestCase("price", Result = "System.Decimal:10.25")]
        [TestCase("Price", Result = "System.Decimal:10.25")]
        [TestCase("PRICE", Result = "System.Decimal:10.25")]
        [TestCase("discountrate", Result = "System.Decimal:0.5")]
        [TestCase("DiscountRate", Result = "System.Decimal:0.5")]
        [TestCase("DISCOUNTRATE", Result = "System.Decimal:0.5")]
        [TestCase("rate", Result = "System.Decimal:0.5")]
        [TestCase("Rate", Result = "System.Decimal:0.5")]
        [TestCase("RATE", Result = "System.Decimal:0.5")]
        public string EvaluateShouldReturnCorrectValue(string propertyName)
        {
            var entity = new Entity { Id = 1, Name = "Pen", UnitPrice = 10.25m, DiscountRate = 0.5, Description = "Blank Pen" };

           var operand =  testee.Evaluate(Expression.Constant(entity), propertyName);
           var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsTrue(actual.GetType() == operand.OperandType);

           return actual.GetType().FullName + ":" + actual.ToString();
        }


        public class Entity
        {
            public int Id { get; set; }

            [Alias("n", Description="Item Name")]
            public string Name { get; set; }

            [Alias("p")]
            [Alias("price")]
            [System.ComponentModel.Description("Unit Price")]
            public decimal UnitPrice { get; set; }

            [Alias("rate")]
            public double? DiscountRate { get; set; }

            [NotEvaluate]
            public string Description { get; set; }
        }
    }
}
