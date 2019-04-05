using System;
using System.Collections.Generic;

namespace CC_Lib.Structures.Graphs
{
        public class GraphNode<TK, TV> : IEquatable<GraphNode<TK, TV>>
        {
            public TK Key { get; }

            public TV Value { get; set; }

            public GraphNode(TK key, TV value)
            {
                Key = key;
                Value = value;
            }

            public bool Equals(GraphNode<TK,TV> other)
            {
                if (other is null) return false;
                return ReferenceEquals(this, other) || EqualityComparer<TK>.Default.Equals(Key, other.Key);
            }

            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((GraphNode<TK, TV>)obj);
            }

            public override int GetHashCode()
            {
                return EqualityComparer<TK>.Default.GetHashCode(Key);
            }

            public static bool operator ==(GraphNode<TK, TV> left, GraphNode<TK, TV> right)
            {
                return left is null && right is null || !(left is null) && left.Equals(right);
            }

            public static bool operator !=(GraphNode<TK, TV> left, GraphNode<TK, TV> right)
            {
                return !(left == right);
            }
    }
}
