using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public class Collection<T> : System.Collections.ObjectModel.Collection<T>
    {
        public Collection()
        {
        }

        public Collection(IList<T> list)
            : base(list)
        {
        }

        public void AddRange(IList<T> list)
        {
            foreach (var item in list)
                Add(item);
        }
    }
}
