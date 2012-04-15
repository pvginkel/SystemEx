using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public class ReversedComparer<T> : IComparer<T>
    {
        private IComparer<T> _comparer;

        public ReversedComparer(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return -_comparer.Compare(x, y);
        }
    }
}
