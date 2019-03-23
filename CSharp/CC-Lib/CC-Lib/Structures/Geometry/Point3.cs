using System;

namespace CC_Lib.Structures.Geometry
{
    public struct Point3 : IComparable<Point3>, IEquatable<Point3>
    {
        public Point3(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Properties

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double SquaredLength => X * X + Y * Y + Z * Z;

        public double Length => Math.Sqrt(SquaredLength);

        public Point3 Normalized
        {
            get
            {
                var length = Length;
                return new Point3(X / length, Y / length, Z / length);
            }
        }

        #endregion

        #region overrides

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        public int CompareTo(Point3 other)
        {
            var xComparison = X.CompareTo(other.X);
            if (xComparison != 0) return xComparison;
            var yComparison = Y.CompareTo(other.Y);
            return yComparison != 0 ? yComparison : Z.CompareTo(other.Z);
        }

        public bool Equals(Point3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj.GetType() == GetType() && Equals((Point3)obj);
        }

        #endregion

        #region methods

        public Point3 CrossProduct(Point3 other)
        {
            return new Point3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y - other.X);
        }

        #endregion

        #region operators

        public static bool operator ==(Point3 left, Point3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point3 left, Point3 right)
        {
            return !left.Equals(right);
        }

        public static Point3 operator +(Point3 left, Point3 right)
        {
            return new Point3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Point3 operator -(Point3 left, Point3 right)
        {
            return new Point3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Point3 operator *(Point3 left, double k)
        {
            return new Point3(left.X * k, left.Y * k, left.Z * k);
        }

        public static double operator *(Point3 left, Point3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static Point3 operator /(Point3 left, double k)
        {
            return new Point3(left.X / k, left.Y / k, left.Z / k);
        }

        #endregion

    }
}
