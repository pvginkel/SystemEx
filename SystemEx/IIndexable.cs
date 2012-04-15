using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    [Obsolete("Use KeyedCollection")]
    public interface IIndexable
    {
        string GetIndexKey();
    }
}
