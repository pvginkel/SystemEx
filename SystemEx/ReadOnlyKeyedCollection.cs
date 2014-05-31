using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SystemEx
{
    public static class ReadOnlyKeyedCollection
    {
        public static ReadOnlyKeyedCollection<TKey, TItem> Create<TKey, TItem>(IKeyedCollection<TKey, TItem> source)
        {
            return new ReadOnlyKeyedCollection<TKey, TItem>(source);
        }
    }

    [Serializable]
    public sealed class ReadOnlyKeyedCollection<TKey, TItem> : IKeyedCollection<TKey, TItem>, IList
    {
        private readonly IKeyedCollection<TKey, TItem> _source;

        public ReadOnlyKeyedCollection(IKeyedCollection<TKey, TItem> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            _source = source;
        }

        public void Add(TItem item)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public void Clear()
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public bool Contains(TItem item)
        {
            return _source.Contains(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            _source.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ((ICollection)_source).Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(TItem item)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_source).GetEnumerator();
        }

        public int IndexOf(TItem item)
        {
            return _source.IndexOf(item);
        }

        public void Insert(int index, TItem item)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public void RemoveAt(int index)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public TItem this[int index]
        {
            get { return ((IList<TItem>)_source)[index]; }
            set
            {
                throw new InvalidOperationException("Collection is read only");
            }
        }

        int IList.Add(object value)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        bool IList.Contains(object value)
        {
            return ((IList)_source).Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)_source).IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        bool IList.IsFixedSize
        {
            get { return ((IList)_source).IsFixedSize; }
        }

        void IList.Remove(object value)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        object IList.this[int index]
        {
            get { return ((IList)_source)[index]; }
            set
            {
                throw new InvalidOperationException("Collection is read only");
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_source).CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_source).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)_source).SyncRoot; }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get { return _source.Comparer; }
        }

        public TItem this[TKey key]
        {
            get { return _source[key]; }
        }

        public bool Contains(TKey key)
        {
            return _source.Contains(key);
        }

        public bool Remove(TKey key)
        {
            throw new InvalidOperationException("Collection is read only");
        }

        public bool TryGetValue(TKey key, out TItem item)
        {
            return _source.TryGetValue(key, out item);
        }
    }
}
