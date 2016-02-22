using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Tests.Instrumentation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation.Objects.Evaluators;

namespace Fidely.Framework.Tests
{
    [TestFixture]
    public class SearchQueryCompilerTest
    {
        private SearchQueryCompiler<ItemEntity> testee;


        [SetUp]
        public void SetUp()
        {
            var setting = new CompilerSetting();
            setting.Operators.Add(new PartialMatch<ItemEntity>(":", false, OperatorIndependency.Strong, "contains"));
            setting.Operators.Add(new LessThan<ItemEntity>("<", OperatorIndependency.Strong, "less than"));
            setting.Operators.Add(new LessThanOrEqual<ItemEntity>("<=", OperatorIndependency.Strong, "less than or equal"));
            setting.Operators.Add(new Equal<ItemEntity>("=", false, OperatorIndependency.Strong, "equal"));
            setting.Operators.Add(new Equal<ItemEntity>("==", false, OperatorIndependency.Strong, "equal"));
            setting.Operators.Add(new NotEqual<ItemEntity>("!=", false, OperatorIndependency.Strong, "not equal"));
            setting.Operators.Add(new Add("+", 1));
            setting.Operators.Add(new Subtract("-", 1));
            setting.Operators.Add(new Multiply("*", 0));
            setting.Operators.Add(new Divide("/", 0));
            setting.Operators.Add(new LessThan<ItemEntity>("lt", OperatorIndependency.Weak, description: "less than"));
            setting.Operators.Add(new LessThanOrEqual<ItemEntity>("le", OperatorIndependency.Weak, description: "less than or equal"));
            setting.Operators.Add(new Add("add", 1, OperatorIndependency.Weak, null));
            setting.Operators.Add(new Multiply("multiply", 0, OperatorIndependency.Weak, null));

            setting.Evaluators.Add(new PropertyEvaluator<ItemEntity>());
            setting.Evaluators.Add(new TypeConversionEvaluator());

            testee = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<ItemEntity>(setting);
        }


        [TestCase(null, Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("o", Result = "1,2,4,5,6,9")]
        [TestCase("5", Result = "1,2,4,5,7,9")]
        [TestCase("ID:5", Result = "5")]
        [TestCase("price:5", Result = "1,2,4,7,9")]
        [TestCase("id:4 AND p:5", Result = "4")]
        [TestCase("(id:4 OR id:6 OR id:9) AND p:5", Result = "4,9")]
        [TestCase("< 5000", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("price < 5000", Result = "0,1,2,5,6")]
        [TestCase("price <= 5000", Result = "0,1,2,4,5,6,7")]
        [TestCase("1000 < price < 5000", Result = "5,6")]
        [TestCase("price < id * 1000", Result = "1,2,5,6,7")]
        [TestCase("price * rate < 5000", Result = "0,2,3,4,5,6,8")]
        [TestCase("Name : Name", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("Name : 'Name'", Result = "3")]
        [TestCase("<5000", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("price<5000", Result = "0,1,2,5,6")]
        [TestCase("price<=5000", Result = "0,1,2,4,5,6,7")]
        [TestCase("1000<price<5000", Result = "5,6")]
        [TestCase("price<id*1000", Result = "1,2,5,6,7")]
        [TestCase("price*rate<5000", Result = "0,2,3,4,5,6,8")]
        [TestCase("Name:Name", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("Name:'Name'", Result = "3")]
        [TestCase("lt 5000", Result = "0,1,2,3,4,5,6,7,8,9")]
        [TestCase("price lt 5000", Result = "0,1,2,5,6")]
        [TestCase("price le 5000", Result = "0,1,2,4,5,6,7")]
        [TestCase("1000 lt price lt 5000", Result = "5,6")]
        [TestCase("price lt id multiply 1000", Result = "1,2,5,6,7")]
        [TestCase("price multiply rate lt 5000", Result = "0,2,3,4,5,6,8")]
        [TestCase("lt5000", Result = "")]
        [TestCase("pricelt5000", Result = "")]
        [TestCase("pricele5000", Result = "")]
        [TestCase("1000ltpricelt5000", Result = "")]
        [TestCase("priceltidmultiply1000", Result = "")]
        [TestCase("pricemultiplyratelt5000", Result = "")]
        public string CompileShouldReturnCorrectExpression(string query)
        {
            var entities = new List<ItemEntity>();
            entities.Add(new ItemEntity { Id = 0, Name = "Pen", UnitPrice = 100, DiscountRate = 1.0 });
            entities.Add(new ItemEntity { Id = 1, Name = "Novel", UnitPrice = 500, DiscountRate = 15 });
            entities.Add(new ItemEntity { Id = 2, Name = "Notebook", UnitPrice = 150, DiscountRate = 1.0 });
            entities.Add(new ItemEntity { Id = 3, Name = "Name Printer", UnitPrice = 30000 });
            entities.Add(new ItemEntity { Id = 4, Name = "Keyboard", UnitPrice = 5000, DiscountRate = 0.5 });
            entities.Add(new ItemEntity { Id = 5, Name = "Mouse", UnitPrice = 2000, DiscountRate = 1.0 });
            entities.Add(new ItemEntity { Id = 6, Name = "Flash Memory", UnitPrice = 3000, DiscountRate = 1.0 });
            entities.Add(new ItemEntity { Id = 7, Name = "CD-RW", UnitPrice = 5000, DiscountRate = 1.0 });
            entities.Add(new ItemEntity { Id = 8, Name = "Hard Disk", UnitPrice = 10000, DiscountRate = 0.1 });
            entities.Add(new ItemEntity { Id = 9, Name = "Portable DVD Drive", UnitPrice = 25000, DiscountRate = 1.0 });

            var result = testee.Compile(query);

            return String.Join(",", entities.AsQueryable().Where(result).Select(o => o.Id));
        }

        [Test]
        public void CompileShouldThrowExceptionWhenDefaultComparativeOperatorIsNotDefined()
        {
            var setting = new CompilerSetting();
            var testee = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<ItemEntity>(setting);
            Assert.Throws<InvalidOperationException>(() => testee.Compile("abc"));
        }
    }
}
