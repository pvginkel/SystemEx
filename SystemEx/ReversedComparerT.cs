using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public class ReversedComparer<T> : Comparer<T>
    {
        private Comparer<T> _comparer;

        public ReversedComparer(Comparer<T> comparer)
        {
            _comparer = comparer;
        }

        public override int Compare(T x, T y)
        {
            return -_comparer.Compare(x, y);
        }
    }
}
