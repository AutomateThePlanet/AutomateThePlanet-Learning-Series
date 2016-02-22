using NUnit.Framework;
using System.Linq.Expressions;
using Fidely.Framework.Compilation.Objects.Operators;
using System;
using Fidely.Framework.Compilation.Objects.Tests.Instrumentation;
using System.Collections.Generic;
using Fidely.Framework.Compilation;

namespace Fidely.Framework.Compilation.Objects.Tests.Operators
{
    [TestFixture]
    public class PartialMatchTest
    {
        private PartialMatch<CustomEntity> testee;


        [SetUp]
        public void SetUp()
        {
            testee = new PartialMatch<CustomEntity>(":");
        }

        [TestCaseSource(typeof(TestCaseDataGenerator), "Generate")]
        public bool CompareShouldReturnCorrectValue(Operand left, Operand right)
        {
            var instance = new CustomEntity
            {
                Decimal = 10m,
                Guid = new Guid("0A740AE1-7B47-467C-9149-D618C5D999F8"),
                DateTime = new DateTime(2000, 12, 12),
                DateTimeOffset = DateTimeOffset.Parse("2020/2/2"),
                TimeSpan = TimeSpan.FromDays(5),
                String = "Entity"
            };
            var expression = Expression.Constant(instance);

            var result = testee.Compare(expression, left, right);
            return (bool)Expression.Lambda(result.Expression).Compile().DynamicInvoke();
        }

        [TestCase("ABC", "b", false, Result = false)]
        [TestCase("ABC", "b", true, Result = true)]
        [TestCase("abc", "b", false, Result = true)]
        [TestCase("abc", "b", true, Result = true)]
        public bool CompareShouldBeAffectedIgnoreCase(string left, string right, bool ignoreCase)
        {
            var expression = Expression.Constant("");
            var result = new PartialMatch<CustomEntity>(":", ignoreCase).Compare(expression, OperandBuilder.BuildUp(left), OperandBuilder.BuildUp(right));
            return (bool)Expression.Lambda(result.Expression).Compile().DynamicInvoke();
        }


        private class TestCaseDataGenerator : BaseTestCaseDataGenerator
        {
            public IEnumerable<TestCaseData> Generate()
            {
                yield return BuildUpTestCaseData(123m, 1m).Returns(true);
                yield return BuildUpTestCaseData(123m, 4m).Returns(false);
                yield return BuildUpTestCaseData(123m, new Guid("D201E013-6DF2-456A-9C24-7261C36AB853")).Returns(false);
                yield return BuildUpTestCaseData(123m, new DateTime(2011, 1, 1)).Returns(false);
                yield return BuildUpTestCaseData(123m, DateTimeOffset.Parse("2011/1/1")).Returns(false);
                yield return BuildUpTestCaseData(123m, TimeSpan.FromDays(1)).Returns(false);
                yield return BuildUpTestCaseData(123m, "2").Returns(true);
                yield return BuildUpTestCaseData(123m, "5").Returns(false);

                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), 1m).Returns(true);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), 9m).Returns(false);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC")).Returns(true);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), new Guid("CBDBF905-8B27-4F26-BFF4-E32DEC9195E2")).Returns(false);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), new DateTime(2011, 1, 1)).Returns(false);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), DateTimeOffset.Parse("2011/1/1")).Returns(false);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), TimeSpan.FromDays(1)).Returns(false);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), "06bc").Returns(true);
                yield return BuildUpTestCaseData(new Guid("C513D666-06BC-4136-B32D-7818AF4B34BC"), "XXX").Returns(false);

                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), 1m).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), 5m).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new Guid("6A5066FE-3726-476C-B229-FFB6AC9347CE")).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new DateTime(2011, 1, 2)).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), DateTimeOffset.Parse("2011/1/1")).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), TimeSpan.FromDays(1)).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), "AM").Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), "PM").Returns(false);

                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), 1m).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), 5m).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new Guid("C7FA0EB3-268D-48B8-ADC5-395EB62A91A9")).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new DateTime(2011, 1, 2)).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), DateTimeOffset.Parse("2011/1/1")).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), DateTimeOffset.Parse("2011/1/2")).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), TimeSpan.FromDays(1)).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), "AM").Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), "PM").Returns(false);

                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), 1m).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), 5m).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new Guid("26016F90-CA47-434E-B254-8B88FCC924A3")).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new DateTime(2011, 1, 1)).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), DateTimeOffset.Parse("2011/1/1")).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), TimeSpan.FromDays(2)).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), "00").Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), "11").Returns(false);

                yield return BuildUpTestCaseData("___ 123 ___", 2m).Returns(true);
                yield return BuildUpTestCaseData("___ 123 ___", 4m).Returns(false);
                yield return BuildUpTestCaseData("___ 044cd12e-a4f8-4855-a5e4-ea99f0a3d5a3 ___", new Guid("044CD12E-A4F8-4855-A5E4-EA99F0A3D5A3")).Returns(true);
                yield return BuildUpTestCaseData("___ 044cd12e-a4f8-4855-a5e4-ea99f0a3d5a3 ___", new Guid("BB661D99-7E4E-4496-8F6C-C79E4A97D195")).Returns(false);
                yield return BuildUpTestCaseData("___ 2011-01-01 12:00:00 AM ___", new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData("___ 2011-01-01 12:00:00 AM ___", new DateTime(2011, 1, 2)).Returns(false);
                yield return BuildUpTestCaseData("___ 2011-01-01 12:00:00 AM +09:00 ___", DateTimeOffset.Parse("2011/1/1")).Returns(true);
                yield return BuildUpTestCaseData("___ 2011-01-01 12:00:00 AM +09:00 ___", DateTimeOffset.Parse("2011/1/2")).Returns(false);
                yield return BuildUpTestCaseData("___ 1.00:00:00 ___", TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData("___ 1.00:00:00 ___", TimeSpan.FromDays(2)).Returns(false);
                yield return BuildUpTestCaseData("___ abc ___", "b").Returns(true);
                yield return BuildUpTestCaseData("___ abc ___", "d").Returns(false);

                yield return BuildUpTestCaseData(1m).Returns(true);
                yield return BuildUpTestCaseData(new Guid("0A740AE1-7B47-467C-9149-D618C5D999F8")).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2000, 12, 12)).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2020/2/2")).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(5)).Returns(true);
                yield return BuildUpTestCaseData("Entity").Returns(true);
            }
        }
    }
}
