using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CC_Lib.Structures.Nodes;

namespace CC_Lib.Structures.Trees
{
    public class BinaryTree<T> : ICollection<T>
        where T : IComparable<T>
    {

        public int Count { get; private set; }

        public bool IsReadOnly { get; } = false;

        public BinaryNode<T> Root { get; private set; }

        public void Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null");
            }

            if (Root is null)
            {
                Root = new BinaryNode<T>(value);
                return;
            }

            var node = Root;

            while (true)
            {
                var cmp = value.CompareTo(node.Value);
                if (cmp == 0)
                {
                    throw new ArgumentException($"Value {value} already exists!", nameof(value));
                }
                if (cmp < 0)
                {
                    if (node.LeftChild is null)
                    {
                        node.LeftChild = new BinaryNode<T>(value) {Parent = node};
                        break;
                    }
                    node = node.LeftChild;
                }
                else
                {
                    if (node.RightChild is null)
                    {
                        node.RightChild = new BinaryNode<T>(value) {Parent = node};
                        break;
                    }
                    node = node.RightChild;
                }
            }

            Count++;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return GetNode(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var arr = this.ToArray();

            for (; arrayIndex < arr.Length; arrayIndex++)
            {
                array[arrayIndex] = arr[arrayIndex];
            }
        }

        public bool Remove(T item)
        {
            var node = GetNode(item);

            if (node is null)
            {
                return false;
            }

            var parent = node.Parent;
            if (parent.LeftChild.Value.CompareTo(item) == 0)
            {
                parent.LeftChild = null;
            }
            else
            {
                parent.RightChild = null;
            }

            Count--;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var stack = new Stack<BinaryNode<T>>();
            var list = new List<T>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.LeftChild;
                }
                else
                {
                    node = stack.Pop();
                    list.Add(node.Value);
                    node = node.RightChild;
                }
            }

            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var stack = new Stack<BinaryNode<T>>();
            var index = 0;

            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.LeftChild;
                }
                else
                {
                    node = stack.Pop();
                    if (node.Value.CompareTo(item) == 0)
                    {
                        return index;
                    }
                    index++;
                    node = node.RightChild;
                }
            }

            return -1;
        }

        public T this[int index]
        {
            get => GetNodeAt(index).Value;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Value can not be null", nameof(value));
                }
                GetNodeAt(index).Value = value;
            }
        }

        private BinaryNode<T> GetNode(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null", nameof(value));
            }

            var node = Root;
            while (node != null)
            {
                var cmp = value.CompareTo(node.Value);
                if (cmp == 0)
                {
                    return node;
                }

                node = cmp < 0 ? node.LeftChild : node.RightChild;
            }

            return null;
        }

        private BinaryNode<T> GetNodeAt(int index)
        {
            if (index < 0)
            {
                index = Count + index;
            }

            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var stack = new Stack<BinaryNode<T>>();
            var counter = 0;

            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.LeftChild;
                }
                else
                {
                    node = stack.Pop();
                    if (counter == index)
                    {
                        return node;
                    }
                    counter++;
                    node = node.RightChild;
                }
            }

            return null;
        }

        public IEnumerable<T> TraversePreOrder()
        {
            return TraversePreOrder(Root.Value);
        }

        public IEnumerable<T> TraversePreOrder(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null", nameof(value));
            }

            var node = GetNode(value);

            var stack = new Stack<BinaryNode<T>>();
            stack.Push(node);
            var list = new List<T>();

            while (stack.Count > 0)
            {
                node = stack.Pop();
                list.Add(node.Value);
                if (node.RightChild != null)
                {
                    stack.Push(node.RightChild);
                }

                if (node.LeftChild != null)
                {
                    stack.Push(node.LeftChild);
                }
            }

            return list;
        }

        public IEnumerable<T> TraverseInOrder()
        {
            return TraverseInOrder(Root.Value);
        }

        public IEnumerable<T> TraverseInOrder(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null", nameof(value));
            }

            var node = GetNode(value);

            var stack = new Stack<BinaryNode<T>>();
            var list = new List<T>();

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    list.Add(node.Value);
                    node = node.LeftChild;
                }
                else
                {
                    node = stack.Pop();
                    stack.Push(node);
                    node = node.RightChild;
                }
            }

            return list;
        }

        public IEnumerable<T> TraversePostOrder()
        {
            return TraversePostOrder(Root.Value);
        }

        public IEnumerable<T> TraversePostOrder(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null", nameof(value));
            }

            var node = GetNode(value);
            BinaryNode<T> lastNode = null;

            var stack = new Stack<BinaryNode<T>>();
            var list = new List<T>();

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.LeftChild;
                }
                else
                {
                    var peekNode = stack.Peek();
                    if (peekNode.RightChild != null && lastNode != peekNode.RightChild)
                    {
                        node = peekNode.RightChild;
                    }
                    else
                    {
                        list.Add(peekNode.Value);
                        lastNode = stack.Pop();
                    }
                }
            }

            return list;
        }

        public IEnumerable<T> TraverseLevelOrder()
        {
            return TraverseLevelOrder(Root.Value);
        }

        public IEnumerable<T> TraverseLevelOrder(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value can not be null", nameof(value));
            }

            var node = GetNode(value);

            var queue = new Queue<BinaryNode<T>>();
            queue.Enqueue(node);
            var list = new List<T>();

            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                list.Add(node.Value);
                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }
            }

            return list;
        }
    }
}
