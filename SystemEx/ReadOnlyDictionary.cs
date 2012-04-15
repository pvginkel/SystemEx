using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;

namespace SystemEx
{
    // From http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=29.

    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(ReadOnlyDictionaryDebugView<,>))]
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        private readonly IDictionary<TKey, TValue> source;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionaryToWrap)
        {
            if (dictionaryToWrap == null)
            {
                throw new ArgumentNullException("dictionaryToWrap");
            }

            this.source = dictionaryToWrap;
        }

        public int Count
        {
            get { return this.source.Count; }
        }

        public ICollection<TKey> Keys
        {
            get { return this.source.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return this.source.Values; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return true; }
        }

        bool IDictionary.IsFixedSize
        {
            get { return true; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return true; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)this.source).Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)this.source).Values; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)this.source).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)this.source).SyncRoot; }
        }

        public TValue this[TKey key]
        {
            get { return this.source[key]; }
            set { throw new InvalidOperationException("Dictionary is read only"); }
        }

        object IDictionary.this[object key]
        {
            get { return ((IDictionary)this.source)[key]; }
            set { throw new InvalidOperationException("Dictionary is read only"); }
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        public bool ContainsKey(TKey key)
        {
            return this.source.ContainsKey(key);
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.source.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(
            KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(
            KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.source)
                .Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
            KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.source)
                .CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        IEnumerator<KeyValuePair<TKey, TValue>>
            IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)this.source)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.source).GetEnumerator();
        }

        void IDictionary.Add(object key, object value)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        void IDictionary.Clear()
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        bool IDictionary.Contains(object key)
        {
            return ((IDictionary)this.source).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)this.source).GetEnumerator();
        }

        void IDictionary.Remove(object key)
        {
            throw new InvalidOperationException("Dictionary is read only");
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)this.source).CopyTo(array, index);
        }
    }

    internal sealed class ReadOnlyDictionaryDebugView<TKey, TValue>
    {
        private IDictionary<TKey, TValue> dict;

        public ReadOnlyDictionaryDebugView(
            ReadOnlyDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            this.dict = dictionary;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] Items
        {
            get
            {
                KeyValuePair<TKey, TValue>[] array =
                    new KeyValuePair<TKey, TValue>[this.dict.Count];
                this.dict.CopyTo(array, 0);
                return array;
            }
        }
    }
}
