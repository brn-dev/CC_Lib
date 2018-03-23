using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CC_Lib.Structures
{
    /// <summary>
    /// A queue which orders its elements by the given priority (ascending)
    /// </summary>
    /// <typeparam name="TP">The type of the priority (a numeric type is recommended)</typeparam>
    /// <typeparam name="TV">The type of the value</typeparam>
    public class PriorityQueue<TP, TV> : IEnumerable<TV>
        where TP : IComparable<TP>
    {

        private readonly LinkedList<KeyValuePair<TP, TV>> _list;

        public PriorityQueue()
        {
            _list = new LinkedList<KeyValuePair<TP, TV>>();
        }

        public int Count => _list.Count;

        /// <summary>
        /// Returns the value of the element at the given index
        /// </summary>
        public TV this[int index] => GetNodeAt(index).Value.Value;

        /// <summary>
        /// Returns the node at the given index (also negative index allowed).
        /// </summary>
        private LinkedListNode<KeyValuePair<TP, TV>> GetNodeAt(int index)
        {
            int count = Count;
            if (index < -count || index >= count)
            {
                throw new IndexOutOfRangeException("Index may only be in the range -count <= index < count");
            }

            if (index < 0)
            {
                index = count + index;
            }
            var node = _list.First;
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
            }
            return node;
        }

        /// <summary>
        /// Adds an element to the priority queue. If there are already elements in the queue which have the same priority as the given priority, 
        /// the element will be inserted as last element with the same priority
        /// </summary>
        /// <param name="value">The value to be added</param>
        /// <param name="priority">The priority determining the position of the element</param>
        /// <returns></returns>
        public int Add(TV value, TP priority)
        {
            int insertIndex = 0;

            var node = _list.First;

            while (node != null)
            {
                if (node.Value.Key.CompareTo(priority) > 0)
                {
                    _list.AddBefore(node, new KeyValuePair<TP, TV>(priority, value));
                    return insertIndex;
                }
                node = node.Next;
                insertIndex++;
            }

            _list.AddLast(new KeyValuePair<TP, TV>(priority, value));
            return insertIndex + 1;
        }

        /// <summary>
        /// Returns the value of the element with the lowest priority without removing it.
        /// </summary>
        public TV PeekLowest()
        {
            var first = _list.First;
            return first != null ? first.Value.Value : default(TV);
        }

        /// <summary>
        /// Returns the value of the element with the lowest priority and removes it.
        /// </summary>
        public TV PollLowest()
        {
            var first = _list.First;
            if (first == null)
            {
                return default(TV);
            }
            _list.RemoveFirst();
            return first.Value.Value;
        }

        /// <summary>
        /// Returns the value of the element with the highest priority without removing it.
        /// </summary>
        public TV PeekHighest()
        {
            var last = _list.Last;
            return last != null ? last.Value.Value : default(TV);
        }

        /// <summary>
        /// Returns the value of the element with the highest priority and removes it.
        /// </summary>
        public TV PollHighest()
        {
            var last = _list.Last;
            if (last == null)
            {
                return default(TV);
            }
            _list.RemoveLast();
            return last.Value.Value;
        }

        /// <summary>
        /// Removes the element at the given index.
        /// </summary>
        public void RemoveAt(int index)
        {
            int count = Count;
            if (index < -count || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
            _list.Remove(GetNodeAt(index));

        }

        public IEnumerator<TV> GetEnumerator()
        {
            return _list.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
