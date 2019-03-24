using System;
using CC_Lib.Structures.Geometry3D;

namespace CC_Lib.Structures.Geometry2D
{
    public struct Vector2 : IComparable<Vector2>, IEquatable<Vector2>
    {
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        #region Properties

        public double X { get; set; }

        public double Y { get; set; }

        public double SquaredLength => X * X + Y * Y;

        public double Length => Math.Sqrt(SquaredLength);

        public Vector2 Normalized
        {
            get
            {
                var length = Length;
                return new Vector2(X / length, Y / length);
            }
        }

        public Vector2 Inverse => new Vector2(X, Y);

        public Vector2 Perpendicular => new Vector2(-Y, X);

        public Vector3 Vector3 => new Vector3(X, Y);

        #endregion

        #region overrides

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({X} {Y})";
        }

        public int CompareTo(Vector2 other)
        {
            var xComparison = X.CompareTo(other.X);
            return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
        }

        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj.GetType() == GetType() && Equals((Vector2)obj);
        }

        #endregion

        #region methods

        #endregion

        #region operators

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(Vector2 left, double k)
        {
            return new Vector2(left.X * k, left.Y * k);
        }

        public static double operator *(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        public static Vector2 operator /(Vector2 left, double k)
        {
            return new Vector2(left.X / k, left.Y / k);
        }

        #endregion

        #region defaults

        public static Vector2 Up { get; } = new Vector2(0, 1);

        public static Vector2 Down { get; } = new Vector2(0, -1);

        public static Vector2 Right { get; } = new Vector2(1, 0);

        public static Vector2 Left { get; } = new Vector2(-1, 0);

        public static Vector2 Zero { get; } = new Vector2(0, 0);

        #endregion

        #region builders

        public static Vector2 FromDirection(Direction direction)
        {
            return Directions.GetVector(direction);
        }
#endregion

    }
}
