using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Objects.Evaluators;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace Fidely.Framework.Compilation.Objects.Tests.Evaluators
{
    [TestFixture]
    public class OperandBuilderTest
    {
        private OperandBuilder testee;


        [SetUp]
        public void SetUp()
        {
            testee = new OperandBuilder();
        }


        [TestCase("Byte", Result = "Decimal:1")]
        [TestCase("Int", Result = "Decimal:2")]
        [TestCase("Double", Result = "Decimal:3")]
        [TestCase("Char", Result = "String:a")]
        [TestCase("Bool", Result = "String:True")]
        [TestCase("Guid", Result = "Guid:4102ad2f-6a9c-4618-ab68-060618a2dd78")]
        [TestCase("DateTime", Result = "DateTime:2010-01-01 12:00:00 AM")]
        [TestCase("DateTimeOffset", Result = "DateTimeOffset:2010-02-02 12:00:00 AM +09:00")]
        [TestCase("TimeSpan", Result = "TimeSpan:1.00:00:00")]
        [TestCase("String", Result = "String:abc")]
        [TestCase("NullableByte", Result = "Decimal:11")]
        [TestCase("NullableInt", Result = "Decimal:12")]
        [TestCase("NullableDouble", Result = "Decimal:13")]
        [TestCase("NullableChar", Result = "String:A")]
        [TestCase("NullableBool", Result = "String:True")]
        [TestCase("NullableGuid", Result = "Guid:8fa4ac7e-0d99-4b47-9921-ca47ae09b709")]
        [TestCase("NullableDateTime", Result = "DateTime:2011-11-11 12:00:00 AM")]
        [TestCase("NullableDateTimeOffset", Result = "DateTimeOffset:2011-12-12 12:00:00 AM +09:00")]
        [TestCase("NullableTimeSpan", Result = "TimeSpan:11.00:00:00")]
        public string BuildUpShouldReturnCorrectOperand(string propertyName)
        {
            var entity = new Entity
            {
                Byte = 1,
                Int = 2,
                Double = 3,
                Char = 'a',
                Bool = true,
                Guid = new Guid("4102AD2F-6A9C-4618-AB68-060618A2DD78"),
                DateTime = new DateTime(2010, 1, 1),
                DateTimeOffset = new DateTime(2010, 2, 2),
                TimeSpan = TimeSpan.FromDays(1),
                String = "abc",
                NullableByte = 11,
                NullableInt = 12,
                NullableDouble = 13,
                NullableChar = 'A',
                NullableBool = true,
                NullableGuid=new Guid("8FA4AC7E-0D99-4B47-9921-CA47AE09B709"),
                NullableDateTime = new DateTime(2011, 11, 11),
                NullableDateTimeOffset = new DateTime(2011, 12, 12),
                NullableTimeSpan = TimeSpan.FromDays(11),
            };

            var operand = testee.BuildUp(Expression.Constant(entity), typeof(Entity).GetProperty(propertyName));
            var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsTrue(actual.GetType() == operand.OperandType);

            return actual.GetType().Name + ":" + actual.ToString();
        }

        [TestCase("NullableByte", Result = "Decimal:0")]
        [TestCase("NullableInt", Result = "Decimal:0")]
        [TestCase("NullableDouble", Result = "Decimal:0")]
        [TestCase("NullableChar", Result = "String:")]
        [TestCase("NullableBool", Result = "String:")]
        [TestCase("NullableGuid", Result = "Guid:00000000-0000-0000-0000-000000000000")]
        [TestCase("NullableDateTime", Result = "DateTime:0001-01-01 12:00:00 AM")]
        [TestCase("NullableDateTimeOffset", Result = "DateTimeOffset:0001-01-01 12:00:00 AM +00:00")]
        [TestCase("NullableTimeSpan", Result = "TimeSpan:00:00:00")]
        public string BuildUpShouldBeAbleToHandleNullableValueCorrectly(string propertyName)
        {
            var entity = new Entity();

            var operand = testee.BuildUp(Expression.Constant(entity), typeof(Entity).GetProperty(propertyName));
            var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsTrue(actual.GetType() == operand.OperandType);

            return actual.GetType().Name + ":" + actual.ToString();
        }

        [Test]
        public void BuildUpShouldReturnEmptyStringWhenPropertyIsNull()
        {
            var entity = new Entity();

            var operand = testee.BuildUp(Expression.Constant(entity), typeof(Entity).GetProperty("String"));
            var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsTrue(actual.GetType() == operand.OperandType);
            Assert.IsEmpty(actual.ToString());
        }

        [TestCaseSource(typeof(TestDataGenerator), "Generate")]
        public string BuildUpShouldBeAbleToHandleNullableThatDoesNotHaveValueCorrectly(object value)
        {
            var operand = testee.BuildUp(value);
            var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsTrue(actual.GetType() == operand.OperandType);

            return actual.GetType().Name + ":" + actual.ToString();
        }

        [TestCase(typeof(byte), Result = true)]
        [TestCase(typeof(sbyte), Result = true)]
        [TestCase(typeof(short), Result = true)]
        [TestCase(typeof(ushort), Result = true)]
        [TestCase(typeof(int), Result = true)]
        [TestCase(typeof(uint), Result = true)]
        [TestCase(typeof(long), Result = true)]
        [TestCase(typeof(ulong), Result = true)]
        [TestCase(typeof(float), Result = true)]
        [TestCase(typeof(double), Result = true)]
        [TestCase(typeof(decimal), Result = true)]
        [TestCase(typeof(object), Result = false)]
        [TestCase(typeof(bool), Result = false)]
        [TestCase(typeof(char), Result = false)]
        [TestCase(typeof(string), Result = false)]
        public bool IsNumberReturnCorrectValue(Type type)
        {
            return testee.IsNumber(type);
        }


        public class Entity
        {
            public byte Byte { get; set; }

            public int Int { get; set; }

            public double Double { get; set; }

            public char Char { get; set; }

            public bool Bool { get; set; }

            public Guid Guid { get; set; }

            public DateTime DateTime { get; set; }

            public DateTimeOffset DateTimeOffset { get; set; }

            public TimeSpan TimeSpan { get; set; }

            public string String { get; set; }

            public byte? NullableByte { get; set; }

            public int? NullableInt { get; set; }

            public double? NullableDouble { get; set; }

            public char? NullableChar { get; set; }

            public bool? NullableBool { get; set; }

            public Guid? NullableGuid { get; set; }

            public DateTime? NullableDateTime { get; set; }

            public DateTimeOffset? NullableDateTimeOffset { get; set; }

            public TimeSpan? NullableTimeSpan { get; set; }
        }


        private static class TestDataGenerator
        {
            public static IEnumerable<TestCaseData> Generate()
            {
                yield return new TestCaseData((byte)1).Returns("Decimal:1");
                yield return new TestCaseData((sbyte)2).Returns("Decimal:2");
                yield return new TestCaseData((short)3).Returns("Decimal:3");
                yield return new TestCaseData((ushort)4).Returns("Decimal:4");
                yield return new TestCaseData((int)5).Returns("Decimal:5");
                yield return new TestCaseData((uint)6).Returns("Decimal:6");
                yield return new TestCaseData((long)7).Returns("Decimal:7");
                yield return new TestCaseData((ulong)8).Returns("Decimal:8");
                yield return new TestCaseData((float)9.5).Returns("Decimal:9.5");
                yield return new TestCaseData((double)10.5).Returns("Decimal:10.5");
                yield return new TestCaseData((decimal)11.5).Returns("Decimal:11.5");
                yield return new TestCaseData(new DateTime(2010,1,1)).Returns("DateTime:2010-01-01 12:00:00 AM");
                yield return new TestCaseData(TimeSpan.FromDays(2)).Returns("TimeSpan:2.00:00:00");
                yield return new TestCaseData(true).Returns("String:True");
                yield return new TestCaseData(false).Returns("String:False");
                yield return new TestCaseData('a').Returns("String:a");
                yield return new TestCaseData(default(Nullable<byte>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<sbyte>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<short>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<ushort>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<int>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<uint>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<long>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<ulong>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<float>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<double>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<decimal>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<DateTime>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<TimeSpan>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<bool>)).Returns("String:");
                yield return new TestCaseData(default(Nullable<char>)).Returns("String:");
            }
        }
    }
}
