/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Caching
{
    internal class Cache<TKey, TValue>
    {
        private readonly int cacheSize;

        private readonly List<CacheItem<TKey, TValue>> items;

        internal Cache(int cacheSize)
        {
            Logger.Info("Initializing cache (size = '{0}').", cacheSize);

            this.cacheSize = (cacheSize > 0) ? cacheSize : 0;
            this.items = new List<CacheItem<TKey, TValue>>();
        }

        internal TValue GetValue(TKey key)
        {
            Logger.Info("Fetching cached data with the specified key '{0}'.", key);

            CacheItem<TKey, TValue> item = this.items.FirstOrDefault(o => o.Key.Equals(key));
            if (item == null)
            {
                Logger.Info("The cached data with the specified key '{0}' wasn't found.", key);
                return default(TValue);
            }

            Logger.Info("Hit cache.");
            item.Hits++;
            return item.Value;
        }

        internal void SetValue(TKey key, TValue value)
        {
            Logger.Info("Caching data '{0}' with the specified key '{1}'.", value, key);

            if (this.items.Count == this.cacheSize)
            {
                this.items.Sort((x, y) => x.Hits.CompareTo(y.Hits));
                this.items.RemoveRange(0, this.items.Count / 4);
                Logger.Verbose("Shrank cache.");
            }

            var cache = new CacheItem<TKey, TValue>(key, value);
            this.items.Add(cache);
        }
    }
}