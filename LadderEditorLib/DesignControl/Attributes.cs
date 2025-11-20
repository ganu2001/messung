using System;
using System.Collections.Generic;
using System.Linq;

namespace LadderDrawing
{
    public class Attributes : List<Attribute>
    {
        private Dictionary<string, int> _attributeIndexCache;
        private const int CACHE_SIZE = 100;
        private Dictionary<string, WeakReference<Attribute>> _weakCache;

        public Attributes()
        {
            _attributeIndexCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            _weakCache = new Dictionary<string, WeakReference<Attribute>>();
        }

        public object this[string name]
        {
            set
            {
                int index = GetOrdinal(name);
                if (index != -1)
                {
                    try
                    {
                        this[index].Value = value;
                    }
                    catch (Exception e)
                    {
                        this[1].Value = value;
                        Console.WriteLine("Exception  - " + e);
                    }
                }
                else
                {
                    Attribute attribute = new Attribute
                    {
                        Name = name,
                        Value = value
                    };
                    this.Add(attribute);
                    UpdateCache(name, this.Count - 1);
                }
            }
            get
            {
                int index = GetOrdinal(name);
                if (index != -1)
                    try
                    {
                        return this[index].Value;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception - " + e);
                        return this[1].Value;
                    }
                return "";
            }
        }

        public int GetOrdinal(string name)
        {
            name = name.Trim();

            // Try cache first
            if (_attributeIndexCache.TryGetValue(name, out int cachedIndex))
            {
                return cachedIndex;
            }

            // Fallback to linear search
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    UpdateCache(name, i);
                    return i;
                }
            }

            return -1;
        }

        private void UpdateCache(string name, int index)
        {
            // Implement LRU cache (simple clear for now)
            if (_attributeIndexCache.Count >= CACHE_SIZE)
            {
                _attributeIndexCache.Clear();
            }

            _attributeIndexCache[name] = index;
        }

        public new void Add(Attribute attribute)
        {
            base.Add(attribute);
            UpdateCache(attribute.Name, this.Count - 1);
        }

        public new int RemoveAll(Predicate<Attribute> match)
        {
            var removedAttributes = this.Where(attr => match(attr)).ToList();

            int removedCount = base.RemoveAll(match);

            foreach (var attr in removedAttributes)
            {
                _attributeIndexCache.Remove(attr.Name);
            }

            RebuildCache();

            return removedCount;
        }
        public new void Clear()
        {
            base.Clear();
            _attributeIndexCache.Clear();
        }

        public void UpdateAttributes(Dictionary<string, object> updates)
        {
            foreach (var update in updates)
            {
                this[update.Key] = update.Value;
            }
        }

        private void RebuildCache()
        {
            _attributeIndexCache.Clear();
            for (int i = 0; i < this.Count; i++)
            {
                _attributeIndexCache[this[i].Name] = i;
            }
        }

        public void Dispose()
        {
            _attributeIndexCache.Clear();
            Clear();
        }
    }
}