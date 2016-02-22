using NUnit.Framework;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation.Objects.Tests.Instrumentation;

namespace Fidely.Framework.Compilation.Objects.Tests.Operators
{
    [TestFixture]
    public class NotEqualTest
    {
        private NotEqual<CustomEntity> testee;


        [SetUp]
        public void SetUp()
        {
            testee = new NotEqual<CustomEntity>("!=");
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

        [TestCase("ABC", "abc", false, Result = true)]
        [TestCase("ABC", "abc", true, Result = false)]
        [TestCase("abc", "abc", false, Result = false)]
        [TestCase("abc", "abc", true, Result = false)]
        public bool CompareShouldBeAffectedIgnoreCase(string left, string right, bool ignoreCase)
        {
            var expression = Expression.Constant("");

            var result = new NotEqual<CustomEntity>("!=", ignoreCase).Compare(expression, OperandBuilder.BuildUp(left), OperandBuilder.BuildUp(right));
            return (bool)Expression.Lambda(result.Expression).Compile().DynamicInvoke();
        }


        private class TestCaseDataGenerator : BaseTestCaseDataGenerator
        {
            public IEnumerable<TestCaseData> Generate()
            {
                yield return BuildUpTestCaseData(1m, 1m).Returns(false);
                yield return BuildUpTestCaseData(1m, 2m).Returns(true);
                yield return BuildUpTestCaseData(1m, new Guid("1CFDDBE0-EAE3-4EB6-81F3-48C5BFADFC88")).Returns(true);
                yield return BuildUpTestCaseData(1m, new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(1m, DateTimeOffset.Parse("2012/1/1")).Returns(true);
                yield return BuildUpTestCaseData(1m, TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData(1m, "abc").Returns(true);

                yield return BuildUpTestCaseData(new Guid("2290A029-F5F6-40D7-AC1A-87AA61D688A4"), new Guid("2290A029-F5F6-40D7-AC1A-87AA61D688A4")).Returns(false);
                yield return BuildUpTestCaseData(new Guid("04A3D794-3901-4AD0-98D9-A77ED6F87727"), 1m).Returns(true);
                yield return BuildUpTestCaseData(new Guid("2290A029-F5F6-40D7-AC1A-87AA61D688A4"), new Guid("0AFA2CFC-5A34-40C8-BD05-1D4021877523")).Returns(true);
                yield return BuildUpTestCaseData(new Guid("83269849-79D6-4A84-803C-4F29B1E19591"), new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(new Guid("529A980D-F2DD-4B4A-AC85-72F5B39A5DA7"), DateTimeOffset.Parse("2012/1/1")).Returns(true);
                yield return BuildUpTestCaseData(new Guid("A72BCD01-ADCE-4C79-8BEC-3D4508D852BA"), TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData(new Guid("4107BBEE-D7F5-41FD-AA34-66E750A76765"), "abc").Returns(true);

                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new DateTime(2011, 1, 1)).Returns(false);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), 1m).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new Guid("305C2BEC-6216-4B84-94DA-112A3FE1B56B")).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new DateTime(2001, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), DateTimeOffset.Parse("2011/1/1")).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), "abc").Returns(true);

                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), DateTimeOffset.Parse("2011/1/1")).Returns(false);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), 1m).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new Guid("C29BE15D-4137-4B6A-A8EE-69E519EA5AB0")).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), DateTimeOffset.Parse("2012/1/1")).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), "abc").Returns(true);

                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), TimeSpan.FromDays(1)).Returns(false);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), 1m).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new Guid("900E1F27-5166-4948-B316-2125B034C652")).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new DateTime(2011, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), DateTimeOffset.Parse("2012/1/1")).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), TimeSpan.FromDays(2)).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), "abc").Returns(true);

                yield return BuildUpTestCaseData("xyz", "xyz").Returns(false);
                yield return BuildUpTestCaseData("xyz", 1m).Returns(true);
                yield return BuildUpTestCaseData("xyz", new Guid("A66741E1-C852-4A36-B0D3-7C6D15FBF734")).Returns(true);
                yield return BuildUpTestCaseData("xyz", new DateTime(2001, 1, 1)).Returns(true);
                yield return BuildUpTestCaseData("xyz", DateTimeOffset.Parse("2012/1/1")).Returns(true);
                yield return BuildUpTestCaseData("xyz", TimeSpan.FromDays(1)).Returns(true);
                yield return BuildUpTestCaseData("xyz", "abc").Returns(true);

                yield return BuildUpTestCaseData(10m).Returns(true);
                yield return BuildUpTestCaseData(new Guid("0A740AE1-7B47-467C-9149-D618C5D999F8")).Returns(true);
                yield return BuildUpTestCaseData(new DateTime(2000, 12, 12)).Returns(true);
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2020/2/2")).Returns(true);
                yield return BuildUpTestCaseData(TimeSpan.FromDays(5)).Returns(true);
                yield return BuildUpTestCaseData("Entity").Returns(true);
                yield return BuildUpTestCaseData(100m).Returns(true);

                yield return BuildUpTestCaseData(null, "").Returns(false);
                yield return BuildUpTestCaseData("", null).Returns(false);
                yield return BuildUpTestCaseData(null, "abc").Returns(true);
                yield return BuildUpTestCaseData("abc", null).Returns(true);
            }
        }
    }
}
