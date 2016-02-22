using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using Fidely.Framework.Compilation.Entities.Evaluators;

namespace Fidely.Framework.Compilation.Entities.Tests.Evaluators
{
    [TestFixture]
    public class EntityPropertyEvaluatorTest
    {
        private PropertyEvaluator<Entity> testee;


        [SetUp]
        public void SetUp()
        {
            testee = new PropertyEvaluator<Entity>();
        }

        [Test]
        public void ConstructorShouldRegisterSupportedTypeProperties()
        {
            //var mapping = typeof(BaseBuiltInEvaluator).GetField("mapping", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testee) as IDictionary<string, PropertyInfo>;
            //var expected = new string[] { "BYTE", "SBYTE", "BOOL", "SHORT", "INT", "LONG", "FLOAT", "DOUBLE", "DECIMAL", "STRING", "GUID", "DATETIME", "DATETIMEOFFSET", "TIMESPAN" };
            //CollectionAssert.AreEquivalent(expected, mapping.Select(o => o.Key).ToList());
            Assert.Fail();
        }


        private class Entity
        {
            public byte Byte { get; set; }
            public sbyte SByte { get; set; }
            public bool Bool { get; set; }
            public short Short { get; set; }
            public int Int { get; set; }
            public long Long { get; set; }
            public float Float { get; set; }
            public double Double { get; set; }
            public decimal Decimal { get; set; }
            public string String { get; set; }
            public Guid Guid { get; set; }
            public DateTime DateTime { get; set; }
            public DateTimeOffset DateTimeOffset { get; set; }
            public TimeSpan TimeSpan { get; set; }
            public char Char { get; set; }
            public byte[] ByteArray { get; set; }
            public List<string> StringList { get; set; }
            public Entity Parent { get; set; }
        }
    }
}
