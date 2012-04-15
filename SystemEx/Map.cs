using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SystemEx
{
    /// <summary>
    /// Represents a generic map of objects of type <typeparamref name="T1"/>
    /// to objects of type <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">Type of the objects to be stored in the map.</typeparam>
    /// <typeparam name="T2">Type of the objects to be stored in the map.</typeparam>
    public class Map<T1, T2> : IMap<T1, T2>
    {
        private readonly Dictionary<T1, Tuple<T1, T2>> _lookup1;
        private readonly Dictionary<T2, Tuple<T1, T2>> _lookup2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map{T1,T2}"/> class.
        /// </summary>
        public Map()
        {
            _lookup1 = new Dictionary<T1, Tuple<T1, T2>>();
            _lookup2 = new Dictionary<T2, Tuple<T1, T2>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map{T1,T2}"/> class
        /// with the specified capacity.
        /// </summary>
        /// <param name="capacity">Capacity to initialize the <see cref="Map{T1,T2}"/>
        /// with.</param>
        public Map(int capacity)
        {
            _lookup1 = new Dictionary<T1, Tuple<T1, T2>>(capacity);
            _lookup2 = new Dictionary<T2, Tuple<T1, T2>>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map{T1,T2}"/> class
        /// with the contents of <paramref name="map"/>.
        /// </summary>
        /// <param name="map">The map to copy all items from into
        /// the new instance.</param>
        public Map(IMap<T1, T2> map)
        {
            if (map == null)
                throw new ArgumentNullException("map");

            var mapObject = map as Map<T1, T2>;

            if (mapObject != null)
            {
                _lookup1 = new Dictionary<T1, Tuple<T1, T2>>(mapObject._lookup1);
                _lookup2 = new Dictionary<T2, Tuple<T1, T2>>(mapObject._lookup2);
            }
            else
            {
                _lookup1 = new Dictionary<T1, Tuple<T1, T2>>(map.Count);
                _lookup2 = new Dictionary<T2, Tuple<T1, T2>>(map.Count);

                foreach (var tuple in map)
                {
                    Add(tuple);
                }
            }
        }

        /// <summary>
        /// Adds a tuple of items to the map.
        /// </summary>
        /// <param name="item1">The first item of the tuple to be added to the map.</param>
        /// <param name="item2">The second item of the tuple to be added to the map.</param>
        public void Add(T1 item1, T2 item2)
        {
            Add(new Tuple<T1, T2>(item1, item2));
        }

        /// <summary>
        /// Adds a tuple to the map.
        /// </summary>
        /// <param name="item">The tuple to add to the map.</param>
        public void Add(Tuple<T1, T2> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (ContainsAny(item))
                throw new ArgumentException("Map already contains this item");

            _lookup1.Add(item.Item1, item);
            _lookup2.Add(item.Item2, item);
        }

        /// <summary>
        /// Clears the map.
        /// </summary>
        public void Clear()
        {
            _lookup1.Clear();
            _lookup2.Clear();
        }

        /// <summary>
        /// Test whether the map contains the specified tuple.
        /// </summary>
        /// <param name="item">The tuple to be looked up in the map.</param>
        /// <returns>true when the tuple is present in the map; otherwise false.</returns>
        public bool Contains(Tuple<T1, T2> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return _lookup1.ContainsKey(item.Item1) && _lookup2.ContainsKey(item.Item2);
        }

        private bool ContainsAny(Tuple<T1, T2> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return _lookup1.ContainsKey(item.Item1) || _lookup2.ContainsKey(item.Item2);
        }

        /// <summary>
        /// Test whether the map contains an instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>true when the instance is presented in the map; otherwise false.</returns>
        public bool ContainsItem1(T1 item)
        {
            return _lookup1.ContainsKey(item);
        }

        /// <summary>
        /// Test whether the map contains an instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>true when the instance is presented in the map; otherwise false.</returns>
        public bool ContainsItem2(T2 item)
        {
            return _lookup2.ContainsKey(item);
        }

        /// <summary>
        /// Removes a tuple from the map.
        /// </summary>
        /// <param name="item">The tuple to be removed from the map.</param>
        public void Remove(Tuple<T1, T2> item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            // Removing items that do not exist in the map are a no-op.

            if (Contains(item))
            {
                _lookup1.Remove(item.Item1);
                _lookup2.Remove(item.Item2);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Tuple<T1, T2>> GetEnumerator()
        {
            return _lookup1.Values.GetEnumerator();
        }

        /// <summary>
        /// Get the number of items in the map.
        /// </summary>
        public int Count
        {
            get { return _lookup1.Count; }
        }

        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all instances in the map
        /// of <b>T1</b>.
        /// </summary>
        public IEnumerable<T1> Item1Keys
        {
            get { return _lookup1.Keys; }
        }

        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all instances in the map
        /// of <b>T2</b>.
        /// </summary>
        public IEnumerable<T2> Item2Keys
        {
            get { return _lookup2.Keys; }
        }

        /// <summary>
        /// Get an <see cref="IEnumerable{T}"/> for all tuples in the map.
        /// </summary>
        public IEnumerable<Tuple<T1, T2>> Tuples
        {
            get { return _lookup1.Values; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Get the item associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The instance associated with <paramref name="item"/>.</returns>
        public T2 GetValueByItem1(T1 item)
        {
            return _lookup1[item].Item2;
        }

        /// <summary>
        /// Get the item associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The instance associated with <paramref name="item"/>.</returns>
        public T1 GetValueByItem2(T2 item)
        {
            return _lookup2[item].Item1;
        }

        /// <summary>
        /// Tries to get the item associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The instance associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        public bool TryGetValueByItem1(T1 item, out T2 result)
        {
            Tuple<T1, T2> tuple;

            if (TryGetTupleByItem1(item, out tuple))
            {
                result = tuple.Item2;
                return true;
            }
            else
            {
                result = default(T2);
                return false;
            }
        }

        /// <summary>
        /// Tries to get the item associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The instance associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        public bool TryGetValueByItem2(T2 item, out T1 result)
        {
            Tuple<T1, T2> tuple;

            if (TryGetTupleByItem2(item, out tuple))
            {
                result = tuple.Item1;
                return true;
            }
            else
            {
                result = default(T1);
                return false;
            }
        }

        /// <summary>
        /// Get the tuple associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The tuple associated with <paramref name="item"/>.</returns>
        public Tuple<T1, T2> GetTupleByItem1(T1 item)
        {
            return _lookup1[item];
        }

        /// <summary>
        /// Get the tuple associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <returns>The tuple associated with <paramref name="item"/>.</returns>
        public Tuple<T1, T2> GetTupleByItem2(T2 item)
        {
            return _lookup2[item];
        }

        /// <summary>
        /// Tries to get the tuple associated with the instance of <typeparamref name="T1"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The tuple associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        public bool TryGetTupleByItem1(T1 item, out Tuple<T1, T2> result)
        {
            return _lookup1.TryGetValue(item, out result);
        }

        /// <summary>
        /// Tries to get the tuple associated with the instance of <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="item">The instance to be looked up in the map.</param>
        /// <param name="result">The tuple associated with <paramref name="item"/>.</param>
        /// <returns>true when <paramref name="item"/> was found in the map; otherwise false.</returns>
        public bool TryGetTupleByItem2(T2 item, out Tuple<T1, T2> result)
        {
            return _lookup2.TryGetValue(item, out result);
        }
    }
}
