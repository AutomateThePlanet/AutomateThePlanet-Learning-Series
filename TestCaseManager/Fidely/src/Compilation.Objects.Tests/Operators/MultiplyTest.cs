using NUnit.Framework;
using System.Linq.Expressions;
using Fidely.Framework.Compilation;
using System.Collections.Generic;
using System;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation.Objects.Tests.Instrumentation;

namespace Fidely.Framework.Compilation.Objects.Tests.Operators
{
    [TestFixture]
    public class MultiplyTest
    {
        private Multiply testee;


        [SetUp]
        public void SetUp()
        {
            testee = new Multiply("*", 0);
        }

        [TestCaseSource(typeof(TestCaseDataGenerator), "Generate")]
        public object CalcualteShouldAddCorrectly(Operand left, Operand right)
        {
            var actual = testee.Calculate(left, right);
            var result = Expression.Lambda(actual.Expression).Compile().DynamicInvoke();
            Assert.AreEqual(result.GetType(), actual.OperandType);
            return result;
        }

        private class TestCaseDataGenerator : BaseTestCaseDataGenerator
        {
            public IEnumerable<TestCaseData> Generate()
            {
                yield return BuildUpTestCaseData(1m, 2m).Returns(2m);
                yield return BuildUpTestCaseData(1m, new Guid("1CFDDBE0-EAE3-4EB6-81F3-48C5BFADFC88")).Returns("1*1cfddbe0-eae3-4eb6-81f3-48c5bfadfc88");
                yield return BuildUpTestCaseData(1m, new DateTime(2011, 1, 1)).Returns("1*2011-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData(1m, DateTimeOffset.Parse("2012/1/1")).Returns("1*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData(1m, TimeSpan.FromDays(1)).Returns("1*1.00:00:00");
                yield return BuildUpTestCaseData(1m, "abc").Returns("1*abc");

                yield return BuildUpTestCaseData(new Guid("04A3D794-3901-4AD0-98D9-A77ED6F87727"), 1m).Returns("04a3d794-3901-4ad0-98d9-a77ed6f87727*1");
                yield return BuildUpTestCaseData(new Guid("2290A029-F5F6-40D7-AC1A-87AA61D688A4"), new Guid("0AFA2CFC-5A34-40C8-BD05-1D4021877523")).Returns("2290a029-f5f6-40d7-ac1a-87aa61d688a4*0afa2cfc-5a34-40c8-bd05-1d4021877523");
                yield return BuildUpTestCaseData(new Guid("83269849-79D6-4A84-803C-4F29B1E19591"), new DateTime(2011, 1, 1)).Returns("83269849-79d6-4a84-803c-4f29b1e19591*2011-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData(new Guid("529A980D-F2DD-4B4A-AC85-72F5B39A5DA7"), DateTimeOffset.Parse("2012/1/1")).Returns("529a980d-f2dd-4b4a-ac85-72f5b39a5da7*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData(new Guid("A72BCD01-ADCE-4C79-8BEC-3D4508D852BA"), TimeSpan.FromDays(1)).Returns("a72bcd01-adce-4c79-8bec-3d4508d852ba*1.00:00:00");
                yield return BuildUpTestCaseData(new Guid("4107BBEE-D7F5-41FD-AA34-66E750A76765"), "abc").Returns("4107bbee-d7f5-41fd-aa34-66e750a76765*abc");

                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), 1m).Returns("2011-01-01 12:00:00 AM*1");
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new Guid("305C2BEC-6216-4B84-94DA-112A3FE1B56B")).Returns("2011-01-01 12:00:00 AM*305c2bec-6216-4b84-94da-112a3fe1b56b");
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), new DateTime(2001, 1, 1)).Returns("2011-01-01 12:00:00 AM*2001-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), DateTimeOffset.Parse("2012/1/1")).Returns("2011-01-01 12:00:00 AM*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), TimeSpan.FromDays(1)).Returns("2011-01-01 12:00:00 AM*1.00:00:00");
                yield return BuildUpTestCaseData(new DateTime(2011, 1, 1), "abc").Returns("2011-01-01 12:00:00 AM*abc");

                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), 1m).Returns("2011-01-01 12:00:00 AM +09:00*1");
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new Guid("C29BE15D-4137-4B6A-A8EE-69E519EA5AB0")).Returns("2011-01-01 12:00:00 AM +09:00*c29be15d-4137-4b6a-a8ee-69e519ea5ab0");
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), new DateTime(2011, 1, 1)).Returns("2011-01-01 12:00:00 AM +09:00*2011-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), DateTimeOffset.Parse("2012/1/1")).Returns("2011-01-01 12:00:00 AM +09:00*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), TimeSpan.FromDays(1)).Returns("2011-01-01 12:00:00 AM +09:00*1.00:00:00");
                yield return BuildUpTestCaseData(DateTimeOffset.Parse("2011/1/1"), "abc").Returns("2011-01-01 12:00:00 AM +09:00*abc");

                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), 1m).Returns("1.00:00:00*1");
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new Guid("900E1F27-5166-4948-B316-2125B034C652")).Returns("1.00:00:00*900e1f27-5166-4948-b316-2125b034c652");
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), new DateTime(2011, 1, 1)).Returns("1.00:00:00*2011-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), DateTimeOffset.Parse("2012/1/1")).Returns("1.00:00:00*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), TimeSpan.FromDays(1)).Returns("1.00:00:00*1.00:00:00");
                yield return BuildUpTestCaseData(TimeSpan.FromDays(1), "abc").Returns("1.00:00:00*abc");

                yield return BuildUpTestCaseData("xyz", 1m).Returns("xyz*1");
                yield return BuildUpTestCaseData("xyz", new Guid("A66741E1-C852-4A36-B0D3-7C6D15FBF734")).Returns("xyz*a66741e1-c852-4a36-b0d3-7c6d15fbf734");
                yield return BuildUpTestCaseData("xyz", new DateTime(2001, 1, 1)).Returns("xyz*2001-01-01 12:00:00 AM");
                yield return BuildUpTestCaseData("xyz", DateTimeOffset.Parse("2012/1/1")).Returns("xyz*2012-01-01 12:00:00 AM +09:00");
                yield return BuildUpTestCaseData("xyz", TimeSpan.FromDays(1)).Returns("xyz*1.00:00:00");
                yield return BuildUpTestCaseData("xyz", "abc").Returns("xyz*abc");

                yield return BuildUpTestCaseData(null, "abc").Returns("*abc");
                yield return BuildUpTestCaseData("abc", null).Returns("abc*");
            }
        }
    }
}
