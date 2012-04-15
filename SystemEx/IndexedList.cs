using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SystemEx
{
    public class IndexedList<T> : IEnumerable<T> where T : IIndexable
    {
        private T[] _items;
        private Dictionary<string, T> _dictionary = null;

        public IndexedList(T[] items)
        {
            if (items == null || items.Length == 0)
            {
                _items = null;
            }
            else
            {
                _items = items;
            }
        }

        public T this[int key]
        {
            get
            {
                if (_items == null)
                    throw new InvalidOperationException("Index list empty");

                return _items[key];
            }
        }

        public T this[string key]
        {
            get
            {
                if (_items == null)
                    throw new InvalidOperationException("Index list empty");

                return GetDictionary()[key];
            }
        }

        public bool Contains(string key)
        {
            if (_items == null)
            {
                return false;
            }
            else
            {
                return GetDictionary().ContainsKey(key);
            }
        }

        public int Count
        {
            get
            {
                if (_items == null)
                {
                    return 0;
                }
                else
                {
                    return _items.Length;
                }
            }
        }

        private Dictionary<string, T> GetDictionary()
        {
            if (_items == null)
            {
                return null;
            }
            else
            {
                if (_dictionary == null)
                {
                    _dictionary = new Dictionary<string, T>();

                    foreach (T item in _items)
                    {
                        _dictionary.Add((item as IIndexable).GetIndexKey(), item);
                    }
                }

                return _dictionary;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_items == null)
                throw new InvalidOperationException("Index list empty");

            return (_items as IEnumerable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
