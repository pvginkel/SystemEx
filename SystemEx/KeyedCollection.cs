using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public abstract class KeyedCollection<TKey, TItem> : System.Collections.ObjectModel.KeyedCollection<TKey, TItem>, IKeyedCollection<TKey, TItem>
    {
        public KeyedCollection()
        {
        }

        public KeyedCollection(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public void AddRange(IEnumerable<TItem> list)
        {
            foreach (var item in list)
                Add(item);
        }

        public bool TryGetValue(TKey key, out TItem item)
        {
            if (Contains(key))
            {
                item = this[key];
                return true;
            }
            else
            {
                item = default(TItem);
                return false;
            }
        }
    }
}
