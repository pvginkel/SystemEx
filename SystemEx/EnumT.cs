using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SystemEx
{
    public class Enum<T>
    {
        public static string Format(object value, string format)
        {
            return Enum.Format(typeof(T), value, format);
        }

        public static string GetName(object value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static string[] GetNames()
        {
            return Enum.GetNames(typeof(T));
        }

        public static Type GetUnderlyingType()
        {
            return Enum.GetUnderlyingType(typeof(T));
        }

        public static List<T> GetValues()
        {
            return new List<T>(new EnumConverter<T>(Enum.GetValues(typeof(T)).GetEnumerator()));
        }

        public static bool IsDefined(object value)
        {
            return Enum.IsDefined(typeof(T), value);
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T Parse(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParse(string value, out T enumValue)
        {
            return TryParse(value, out enumValue, false);
        }

        public static bool TryParse(string value, out T enumValue, bool ignoreCase)
        {
            foreach (string name in Enum<T>.GetNames())
            {
                if (String.Compare(name, value, ignoreCase) == 0)
                {
                    enumValue = Enum<T>.Parse(value, ignoreCase);

                    return true;
                }
            }

            enumValue = default(T);

            return false;
        }

        public static T ToObject(byte value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(long value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(object value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        [CLSCompliant(false)]
        public static T ToObject(sbyte value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(short value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        [CLSCompliant(false)]
        public static T ToObject(uint value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        [CLSCompliant(false)]
        public static T ToObject(ulong value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        [CLSCompliant(false)]
        public static T ToObject(ushort value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        private class EnumConverter<U> : IEnumerable<U>, IEnumerator<U>
        {
            IEnumerator _enumerator;

            public EnumConverter(IEnumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public IEnumerator<U> GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public U Current
            {
                get { return (U)_enumerator.Current; }
            }

            public void Dispose()
            {
                // Nothing to do
            }

            object IEnumerator.Current
            {
                get { return _enumerator.Current; }
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }
        }
    }
}
