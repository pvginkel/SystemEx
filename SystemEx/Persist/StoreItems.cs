using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Persist
{
    public class StoreItems
    {
        private Dictionary<string, object> _items;

        internal StoreItems(Dictionary<string, object> items)
        {
            _items = items;
        }

        public object this[string index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public bool ContainsKey(string index)
        {
            return _items.ContainsKey(index);
        }

        public void Remove(string index)
        {
            _items.Remove(index);
        }
    }
}
