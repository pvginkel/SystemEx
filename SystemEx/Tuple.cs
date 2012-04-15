using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SystemEx
{
    [Serializable]
    public class Tuple<T1, T2> : IComparable
    {
        private readonly T1 _item1;
        private readonly T2 _item2;

        public T1 Item1 { get { return _item1; } }
        public T2 Item2 { get { return _item2; } }

        public Tuple(T1 item1, T2 item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj, EqualityComparer<object>.Default);
        }

        public bool Equals(object other, IEqualityComparer comparer)
        {
            if (other == null) return false;

            Tuple<T1, T2> objTuple = other as Tuple<T1, T2>;

            if (objTuple == null)
            {
                return false;
            }

            return comparer.Equals(_item1, objTuple._item1) && comparer.Equals(_item2, objTuple._item2);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj, Comparer<object>.Default);
        }

        public int CompareTo(object obj, IComparer comparer)
        {
            if (obj == null)
                return 1;

            var other = obj as Tuple<T1, T2>;

            if (other == null)
                throw new ArgumentException("Incorrect type");

            int result = 0;

            result = comparer.Compare(_item1, other._item1);

            if (result != 0)
                return result;

            return comparer.Compare(_item2, other._item2);
        }

        public override int GetHashCode()
        {
            return GetHashCode(EqualityComparer<object>.Default);
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            unchecked
            {
                return ObjectUtil.CombineHashCodes(comparer.GetHashCode(_item1), comparer.GetHashCode(_item2));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            sb.Append(_item1);
            sb.Append(", ");
            sb.Append(_item2);
            sb.Append(")");

            return sb.ToString();
        }
    }
}
