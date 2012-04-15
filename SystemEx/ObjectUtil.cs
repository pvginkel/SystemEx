using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    /// <summary>
    /// Contains utility methods for working with objects.
    /// </summary>
    public static class ObjectUtil
    {
        /// <summary>
        /// Test whether an object equals another object.
        /// </summary>
        /// <remarks>
        /// The objects are tested for equality through the following steps:
        /// <list type="number">
        /// <item>
        /// Whether the objects are the same object using <see cref="Object.ReferenceEquals(Object, Object)"/>;
        /// </item>
        /// <item>
        /// Whether <paramref name="b"/> is of type <typeparamref name="T"/>;
        /// </item>
        /// <item>
        /// Whether <paramref name="b"/> is not null;
        /// </item>
        /// <item>
        /// When <paramref name="a"/> implements <see cref="IEquatable{T}"/>,
        /// that interfaced is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, when <paramref name="a"/> implements <see cref="IComparable{T}"/>,
        /// that interface is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, when <paramref name="a"/> implements <see cref="IComparable"/>,
        /// that interface is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, an exception is thrown.
        /// </item>
        /// </list>
        /// </remarks>
        /// <typeparam name="T">The type of <paramref name="a"/>.</typeparam>
        /// <param name="a">The object to compare with the other object.</param>
        /// <param name="b">The object to compare with the other object.</param>
        /// <returns>true when the objects are equal; otherwise false.</returns>
        public static bool Equals<T>(T a, object b)
            where T : class
        {
            if (ReferenceEquals(a, b))
                return true;

            var other = b as T;

            if (other == null)
                return false;

            return EqualsCore(a, other);
        }

        /// <summary>
        /// Test whether an object equals another object.
        /// </summary>
        /// <remarks>
        /// The objects are tested for equality through the following steps:
        /// <list type="number">
        /// <item>
        /// Whether the objects are the same object using <see cref="Object.ReferenceEquals(Object, Object)"/>;
        /// </item>
        /// <item>
        /// When <paramref name="a"/> implements <see cref="IEquatable{T}"/>,
        /// that interfaced is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, when <paramref name="a"/> implements <see cref="IComparable{T}"/>,
        /// that interface is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, when <paramref name="a"/> implements <see cref="IComparable"/>,
        /// that interface is used to compare <paramref name="a"/> to <paramref name="b"/>;
        /// </item>
        /// <item>
        /// Otherwise, an exception is thrown.
        /// </item>
        /// </list>
        /// </remarks>
        /// <typeparam name="T">The type of <paramref name="a"/>.</typeparam>
        /// <param name="a">The object to compare with the other object.</param>
        /// <param name="b">The object to compare with the other object.</param>
        /// <returns>true when the objects are equal; otherwise false.</returns>
        public static bool Equals<T>(T a, T b)
            where T : class
        {
            if (ReferenceEquals(a, b))
                return true;

            return EqualsCore(a, b);
        }

        private static bool EqualsCore<T>(T a, T b)
            where T : class
        {
            var equatable = a as IEquatable<T>;

            if (equatable != null)
                return EqualsCore(equatable, (IEquatable<T>)b);

            var genericComparable = a as IComparable<T>;

            if (genericComparable != null)
                return ComparableCore(genericComparable, b) == 0;

            var comparable = a as IComparable;

            if (comparable != null)
                return ComparableCore(comparable, b) == 0;

            throw new ArgumentException("Incompatible type");
        }

        private static bool EqualsCore<T>(IEquatable<T> a, T b)
            where T : class
        {
            return a.Equals(b);
        }

        private static int ComparableCore<T>(IComparable<T> a, T b)
            where T : class
        {
            return a.CompareTo((T)b);
        }

        private static int ComparableCore(IComparable a, object b)
        {
            return a.CompareTo(b);
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2)
        {
            return (((hash1 << 5) + hash1) ^ hash2);
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2), hash3);
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2), CombineHashCodes(hash3, hash4));
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <param name="hash5">The fifth hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4, int hash5)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2, hash3, hash4), hash5);
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <param name="hash5">The fifth hash code to be combined.</param>
        /// <param name="hash6">The sixth hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2, hash3, hash4), CombineHashCodes(hash5, hash6));
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <param name="hash5">The fifth hash code to be combined.</param>
        /// <param name="hash6">The sixth hash code to be combined.</param>
        /// <param name="hash7">The seventh hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2, hash3, hash4), CombineHashCodes(hash5, hash6, hash7));
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <param name="hash5">The fifth hash code to be combined.</param>
        /// <param name="hash6">The sixth hash code to be combined.</param>
        /// <param name="hash7">The seventh hash code to be combined.</param>
        /// <param name="hash8">The eighth hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2, hash3, hash4), CombineHashCodes(hash5, hash6, hash7, hash8));
        }

        /// <summary>
        /// Combines multiple hashes into a single hash.
        /// </summary>
        /// <remarks>
        /// This method can be used in an implementation of <see cref="Object.GetHashCode()"/>
        /// when the hash code calculation is based on the hash code of multiple other
        /// objects.
        /// </remarks>
        /// <param name="hash1">The first hash code to be combined.</param>
        /// <param name="hash2">The second hash code to be combined.</param>
        /// <param name="hash3">The third hash code to be combined.</param>
        /// <param name="hash4">The fourth hash code to be combined.</param>
        /// <param name="hash5">The fifth hash code to be combined.</param>
        /// <param name="hash6">The sixth hash code to be combined.</param>
        /// <param name="hash7">The seventh hash code to be combined.</param>
        /// <param name="hash8">The eighth hash code to be combined.</param>
        /// <param name="hash9">The ninth hash code to be combined.</param>
        /// <returns>The combined hash code.</returns>
        public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8, int hash9)
        {
            return CombineHashCodes(CombineHashCodes(hash1, hash2, hash3, hash4), CombineHashCodes(CombineHashCodes(hash5, hash6, hash7, hash8), hash9));
        }
    }
}
