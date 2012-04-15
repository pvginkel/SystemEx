using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    /// <summary>
    /// Represents a generic map of objects of type <typeparamref name="T1"/>
    /// to objects of type <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">Type of the objects to be stored in the map.</typeparam>
    /// <typeparam name="T2">Type of the objects to be stored in the map.</typeparam>
    public interface IMap<T1, T2> : IEnumerable<Tuple<T1, T2>>
    {
        /// <summary>
        /// Adds a tuple of items to the map.
        /// </summary>
        /// <param name="item1">The first item of the tuple to be added to the map.</param>
        /// <param name="item2">The second item of the tuple to be added to the map.</param>
        void Add(T1 item1, T2 item2);
        /// <summary>
        /// Adds a tuple to the map.
        /// </summary>
        /// <param name="item">The tuple to add to the map.</param>
        void Add(Tuple<T1, T2> item);
        /// <summary>
        /// Clears the map.
        /// </summary>
        void Clear();
        /// <summary>
        /// Test whether the map contains the specified tuple.
        /// </summary>
        /// <param name="item">The tuple to be looked up in the map.</param>
        /// <returns>true when the tuple is present in the map; otherwise false.</returns>
        bool Contains(Tuple<T1, T2> item);
        /// <summary>
        /// Test whether the map contains an instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>true when the instance is presented in the map; otherwise false.</returns>
        bool ContainsItem1(T1 item);
        /// <summary>
        /// Test whether the map contains an instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>true when the instance is presented in the map; otherwise false.</returns>
        bool ContainsItem2(T2 item);
        /// <summary>
        /// Removes a tuple from the map.
        /// </summary>
        /// <param name="item">The tuple to be removed from the map.</param>
        void Remove(Tuple<T1, T2> item);
        /// <summary>
        /// Get the item associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The instance associated with <paramref name="item"/>.</returns>
        T2 GetValueByItem1(T1 item);
        /// <summary>
        /// Get the item associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The instance associated with <paramref name="item"/>.</returns>
        T1 GetValueByItem2(T2 item);
        /// <summary>
        /// Tries to get the item associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The instance associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        bool TryGetValueByItem1(T1 item, out T2 result);
        /// <summary>
        /// Tries to get the item associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The instance associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        bool TryGetValueByItem2(T2 item, out T1 result);
        /// <summary>
        /// Get the tuple associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The tuple associated with <paramref name="item"/>.</returns>
        Tuple<T1, T2> GetTupleByItem1(T1 item);
        /// <summary>
        /// Get the tuple associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The tuple associated with <paramref name="item"/>.</returns>
        Tuple<T1, T2> GetTupleByItem2(T2 item);
        /// <summary>
        /// Tries to get the tuple associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The tuple associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        bool TryGetTupleByItem1(T1 item, out Tuple<T1, T2> result);
        /// <summary>
        /// Tries to get the tuple associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The tuple associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        bool TryGetTupleByItem2(T2 item, out Tuple<T1, T2> result);
        /// <summary>
        /// Get the number of items in the map.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all instances in the map
        /// of type <b>T1</b>.
        /// </summary>
        IEnumerable<T1> Item1Keys { get; }
        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all instances in the map
        /// of type <b>T2</b>.
        /// </summary>
        IEnumerable<T2> Item2Keys { get; }
        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all tuples in the map.
        /// </summary>
        IEnumerable<Tuple<T1, T2>> Tuples { get; }
    }
}