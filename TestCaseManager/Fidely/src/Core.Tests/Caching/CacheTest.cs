using NUnit.Framework;
using Fidely.Framework.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fidely.Framework.Tests.Caching
{
    [TestFixture]
    public class CacheTest
    {
        private Cache<string, string> testee;

        private List<CacheItem<string, string>> items;


        [SetUp]
        public void SetUp()
        {
            testee = new Cache<string, string>(8);

            var field = testee.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic);
            items = field.GetValue(testee) as List<CacheItem<string, string>>;
        }

        [TestCase("1", Result = "5")]
        [TestCase("2", Result = "6")]
        [TestCase("3", Result = "7")]
        [TestCase("4", Result = "8")]
        [TestCase("5", Result = null)]
        [TestCase("6", Result = null)]
        [TestCase("7", Result = null)]
        [TestCase("8", Result = null)]
        public string GetValueShouldReturnCorrectValue(string key)
        {
            items.Add(new CacheItem<string, string>("1", "5"));
            items.Add(new CacheItem<string, string>("2", "6"));
            items.Add(new CacheItem<string, string>("3", "7"));
            items.Add(new CacheItem<string, string>("4", "8"));

            return testee.GetValue(key);
        }

        [Test]
        public void GetValueShouldIncrementHitCount()
        {
            items.Add(new CacheItem<string, string>("1", "5"));

            Assert.AreEqual(0, items[0].Hits);

            for (int i = 0; i < 10; i++)
            {
                testee.GetValue("1");
            }

            Assert.AreEqual(10, items[0].Hits);
        }

        [Test]
        public void SetValueShouldAddCacheItem()
        {
            items.Add(new CacheItem<string, string>("1", "5"));
            items.Add(new CacheItem<string, string>("2", "6"));
            items.Add(new CacheItem<string, string>("3", "7"));
            items.Add(new CacheItem<string, string>("4", "8"));

            testee.SetValue("a", "b");

            Assert.AreEqual(5, items.Count);
            Assert.AreEqual("a", items[4].Key);
            Assert.AreEqual("b", items[4].Value);
        }

        [Test]
        public void SetValueShouldRemoveLittleUsedCaches()
        {
            items.Add(new CacheItem<string, string>("a", "AAA") { Hits = 3 });
            items.Add(new CacheItem<string, string>("b", "BBB") { Hits = 1 });
            items.Add(new CacheItem<string, string>("c", "CCC") { Hits = 7 });
            items.Add(new CacheItem<string, string>("d", "DDD") { Hits = 11 });
            items.Add(new CacheItem<string, string>("e", "EEE") { Hits = 6 });
            items.Add(new CacheItem<string, string>("f", "FFF") { Hits = 5 });
            items.Add(new CacheItem<string, string>("g", "GGG") { Hits = 2 });
            items.Add(new CacheItem<string, string>("h", "HHH") { Hits = 4 });

            testee.SetValue("i", "III");

            var expected = new string[] { "a", "h", "f", "e", "c", "d", "i"};
            CollectionAssert.AreEqual(expected, items.Select(o => o.Key).ToArray());
        }
    }
}
