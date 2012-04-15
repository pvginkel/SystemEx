using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Persist
{
    public class CommitStoreEventArgs : EventArgs
    {
        private StoreItems _items;

        public CommitStoreEventArgs(StoreItems items)
        {
            _items = items;
        }

        public StoreItems Items
        {
            get { return _items; }
        }
    }

    public delegate void CommitStoreHandler(object sender, CommitStoreEventArgs e);
}
