using CC_Lib.Structures.Geometry2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable FormatStringProblem

namespace CC_Lib.ExtensionMethods
{
    /// <summary>
    /// Extension methods for 2 dimensional arrays.
    /// All functions assume that the first index is the y coordinate (the row - starting from the top) and the second index is the x coordinate (the column) 
    /// </summary>
    public static class MatrixExtensions
    {
        private class MatrixWrapper<T> : IEnumerable<T>
        {
            private readonly T[][] _arr;

            private readonly T[,] _matrix;

            public MatrixWrapper(T[][] arr)
            {
                _arr = arr ?? throw new ArgumentNullException(nameof(arr));
                _matrix = null;
            }

            public MatrixWrapper(T[,] matrix)
            {
                _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
                _arr = null;
            }

            public T this[int y, int x]
            {
                get => _arr != null ? _arr[y][x] : _matrix[y, x];
                set
                {
                    if (_arr != null)
                    {
                        _arr[y][x] = value;
                    }
                    else
                    {
                        _matrix[y, x] = value;
                    }
                }
            }

            public int LengthY => _arr?.Length ?? _matrix.GetLength(0);

            public int LengthOfRow(int rowIndex = 0) => _arr?[rowIndex].Length ?? _matrix.GetLength(1);

            public IEnumerator<T> GetEnumerator()
            {
                var list = new List<T>();
                for (var y = 0; y < LengthY; y++)
                {
                    for (var x = 0; x < LengthOfRow(y); x++)
                    {
                        list.Add(this[y,x]);
                    }
                }

                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #region GetColumn & GetRow

        private static T[] GetColumn<T>(MatrixWrapper<T> matrix, int colIndex)
        {
            var col = new T[matrix.LengthY];
            for (var i = 0; i < col.Length; i++)
            {
                col[i] = matrix[i, colIndex];
            }
            return col;

        }

        public static T[] GetColumn<T>(this T[,] matrix, int colIndex)
        {
            if (colIndex < 0 || colIndex >= matrix.GetLength(1))
            {
                throw new IndexOutOfRangeException($"rowIndex is out of range. 0 <= i <= {matrix.GetLength(0) - 1}, but rowIndex was {colIndex}");
            }

            return GetColumn(new MatrixWrapper<T>(matrix), colIndex);
        }

        public static T[] GetColumn<T>(this T[][] matrix, int colIndex)
        {
            if (colIndex < 0)
            {
                throw new IndexOutOfRangeException($"colIndex may not be smaller than 0! (colIndex is {colIndex})");
            }

            for (var i = 0; i < matrix.Length; i++)
            {
                if (colIndex >= matrix[i].Length)
                {
                    throw new IndexOutOfRangeException($"Row {i} does not have a columns with index {colIndex}");
                }
            }

            return GetColumn(new MatrixWrapper<T>(matrix), colIndex);
        }

        private static T[] GetRow<T>(MatrixWrapper<T> matrix, int rowIndex)
        {
            var row = new T[matrix.LengthOfRow(rowIndex)];
            for (var i = 0; i < row.Length; i++)
            {
                row[i] = matrix[rowIndex, i];
            }
            return row;
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0))
            {
                throw new IndexOutOfRangeException($"rowIndex is out of range. 0 <= i <= {matrix.GetLength(1) - 1}, but rowIndex was {rowIndex}");
            }

            return GetRow(new MatrixWrapper<T>(matrix), rowIndex);
        }

        public static T[] GetRow<T>(this T[][] matrix, int rowIndex)
        {
            return GetRow(new MatrixWrapper<T>(matrix), rowIndex);
        }

        #endregion

        #region GetAt

        public static T GetAt<T>(this T[][] matrix, int r, int c)
        {
            return matrix[r][c];
        }

        public static T GetAt<T>(this T[,] matrix, int r, int c)
        {
            return matrix[r, c];
        }

        public static T GetAt<T>(this T[][] matrix, Vector2 point)
        {
            return matrix[(int)point.Y][(int)point.X];
        }

        public static T GetAt<T>(this T[,] matrix, Vector2 point)
        {
            return matrix[(int)point.Y, (int)point.X];
        }

        public static T GetAt<T>(this T[][] matrix, Tuple<int, int> tuple)
        {
            return matrix[tuple.Item1][tuple.Item2];
        }

        public static T GetAt<T>(this T[,] matrix, Tuple<int, int> tuple)
        {
            return matrix[tuple.Item1, tuple.Item2];
        }

        #endregion

        #region Copy

        public static T[,] Copy<T>(this T[,] matrix)
        {
            var copy = new T[matrix.GetLength(0), matrix.GetLength(1)];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        public static T[][] Copy<T>(this T[][] matrix)
        {
            var copy = new T[matrix.Length][];

            for (var rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
            {
                copy[rowIndex] = new T[matrix[rowIndex].Length];
                for (var columnIndex = 0; columnIndex < copy[rowIndex].Length; columnIndex++)
                {
                    copy[rowIndex][columnIndex] = matrix[rowIndex][columnIndex];
                }
            }

            return copy;
        }
        #endregion

        #region Mirror
        public static T[,] MirrorVertically<T>(this T[,] matrix)
        {
            var mirrored = new T[matrix.GetLength(0), matrix.GetLength(1)];
            int rowLength = mirrored.GetLength(1);

            for (int i = 0; i < mirrored.GetLength(0); i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    mirrored[i, rowLength - 1 - j] = matrix[i, j];
                }
            }
            return mirrored;
        }

        public static T[,] MirrorHorizontally<T>(this T[,] matrix)
        {
            var mirrored = new T[matrix.GetLength(0), matrix.GetLength(1)];
            int colLength = mirrored.GetLength(0);

            for (int i = 0; i < colLength; i++)
            {
                for (int j = 0; j < mirrored.GetLength(1); j++)
                {
                    mirrored[colLength - 1 - i, j] = matrix[i, j];
                }
            }
            return mirrored;
        }
        #endregion

        #region Rotation

        public static int[,] RotateCounterClockwise(this int[,] matrix)
        {
            var rotated = new int[matrix.GetLength(1), matrix.GetLength(0)];
            int oldColumnLength = matrix.GetLength(0);
            int oldRowLength = matrix.GetLength(1);
            for (int i = 0; i < oldRowLength; i++)
            {
                for (int j = 0; j < oldColumnLength; j++)
                {
                    rotated[i, j] = matrix[j, oldRowLength - 1 - i];
                }
            }
            return rotated;
        }

        public static int[,] RotateClockwise(this int[,] matrix)
        {
            var rotated = new int[matrix.GetLength(1), matrix.GetLength(0)];
            int oldColumnLength = matrix.GetLength(0);
            int oldRowLength = matrix.GetLength(1);
            for (int i = 0; i < oldRowLength; i++)
            {
                for (int j = 0; j < oldColumnLength; j++)
                {
                    rotated[i, j] = matrix[oldColumnLength - j - 1, i];
                }
            }
            return rotated;
        }

        public static int[,] Rotate180(this int[,] matrix)
        {
            var rotated = new int[matrix.GetLength(0), matrix.GetLength(1)];
            int oldColumnLength = matrix.GetLength(0);
            int oldRowLength = matrix.GetLength(1);
            for (int i = 0; i < rotated.GetLength(0); i++)
            {
                for (int j = 0; j < rotated.GetLength(1); j++)
                {
                    rotated[i, j] = matrix[oldColumnLength - 1 - i, oldRowLength - 1 - j];
                }
            }
            return rotated;
        }

        public static int[,] RotateClockwiseTimes(this int[,] matrix, int times)
        {
            times = times % 4;
            if (times == 0)
            {
                return matrix.Copy();
            }
            if (times < 0)
            {
                times = 4 + times;
            }
            switch (times)
            {
                case 1:
                    return matrix.RotateClockwise();
                case 2:
                    return matrix.Rotate180();
                case 3:
                    return matrix.RotateCounterClockwise();
                default:
                    throw new Exception("Shouldn't happen");
            }
        }
        #endregion

        #region Converting

        private static T[] ToFlat<T>(MatrixWrapper<T> matrix)
        {
            var list = new List<T>();
            ForEach(matrix, elem => list.Add(elem));
            return list.ToArray();
        }

        public static T[] ToFlat<T>(this T[,] matrix)
        {
            return ToFlat(new MatrixWrapper<T>(matrix));
        }

        public static T[] ToFlat<T>(this T[][] matrix)
        {
            return ToFlat(new MatrixWrapper<T>(matrix));
        }

        public static T[][] ToNested<T>(this T[,] matrix)
        {
            var height = matrix.GetLength(0);
            var nestedArr = new T[height][];
            for (var row = 0; row < height; row++)
            {
                nestedArr[row] = matrix.GetRow(row);
            }
            return nestedArr;
        }

        public static T[,] To2DArray<T>(this T[][] nestedArray)
        {
            var height = nestedArray.Length;
            if (nestedArray.Select(row => row.Length).Distinct().Count() > 1)
            {
                throw new ArgumentException("Sub-arrays of nestedArray must be same size!");
            }
            var width = nestedArray[0].Length;
            var array2D = new T[height, width];
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    array2D[rowIndex, colIndex] = nestedArray[colIndex][rowIndex];
                }
            }
            return array2D;
        }

        #endregion

        #region ForEach

        private static void ForEach<T>(MatrixWrapper<T> matrix, Action<T> action)
        {
            for (var row = 0; row < matrix.LengthY; row++)
            {
                var rowLength = matrix.LengthOfRow(row);
                for (var col = 0; col < rowLength; col++)
                {
                    action(matrix[row, col]);
                }
            }
        }

        public static void ForEach<T>(this T[,] matrix, Action<T> action)
        {
            ForEach(new MatrixWrapper<T>(matrix), action);
        }

        public static void ForEach<T>(this T[][] matrix, Action<T> action)
        {
            ForEach(new MatrixWrapper<T>(matrix), action);
        }

        #endregion

        #region All

        private static bool All<T>(MatrixWrapper<T> matrix, Predicate<T> func)
        {
            for (var row = 0; row < matrix.LengthY; row++)
            {
                var rowLength = matrix.LengthOfRow(row);
                for (var col = 0; col < rowLength; col++)
                {
                    if (!func(matrix[row, col]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool All<T>(this T[,] matrix, Predicate<T> func)
        {
            return All(new MatrixWrapper<T>(matrix), func);
        }

        public static bool All<T>(this T[][] matrix, Predicate<T> func)
        {
            return All(new MatrixWrapper<T>(matrix), func);
        }

        #endregion

        #region Enumerate

        public delegate void EnumerateAction<in T>(int x, int y, T elem);

        private static void Enumerate<T>(MatrixWrapper<T> matrix, EnumerateAction<T> action)
        {
            for (var row = 0; row < matrix.LengthY; row++)
            {
                var rowLength = matrix.LengthOfRow(row);
                for (var col = 0; col < rowLength; col++)
                {
                    action(col, row, matrix[row, col]);
                }
            }
        }

        public static void Enumerate<T>(this T[,] matrix, EnumerateAction<T> action)
        {
            Enumerate(new MatrixWrapper<T>(matrix), action);
        }

        public static void Enumerate<T>(this T[][] matrix, EnumerateAction<T> action)
        {
            Enumerate(new MatrixWrapper<T>(matrix), action);
        }
        #endregion

        #region AssignEach

        public delegate T AssignEachFunc<T>(T elem);

        public delegate T AssignEachWithIndexFunc<T>(int x, int y, T elem);

        private static void AssignEach<T>(MatrixWrapper<T> matrix, AssignEachWithIndexFunc<T> assignmentFunc)
        {
            for (var row = 0; row < matrix.LengthY; row++)
            {
                var rowLength = matrix.LengthOfRow(row);
                for (var col = 0; col < rowLength; col++)
                {
                    matrix[row, col] = assignmentFunc(col, row, matrix[row, col]);
                }
            }
        }

        public static void AssignEach<T>(this T[,] matrix, AssignEachFunc<T> assignmentFunc)
        {
            AssignEach(new MatrixWrapper<T>(matrix), (x, y, elem) => assignmentFunc(elem));
        }

        public static void AssignEach<T>(this T[][] matrix, AssignEachFunc<T> assignmentFunc)
        {
            AssignEach(new MatrixWrapper<T>(matrix), (x, y, elem) => assignmentFunc(elem));
        }

        public static void AssignEach<T>(this T[,] matrix, AssignEachWithIndexFunc<T> assignmentFunc)
        {
            AssignEach(new MatrixWrapper<T>(matrix), assignmentFunc);
        }

        public static void AssignEach<T>(this T[][] matrix, AssignEachWithIndexFunc<T> assignmentFunc)
        {
            AssignEach(new MatrixWrapper<T>(matrix), assignmentFunc);
        }
        #endregion

        #region Visualize
        private static string Visualize<T>(this MatrixWrapper<T> matrix)
        {
            var maxLength = (from T value in matrix select value.ToString() into str select str.Length).Concat(new[] { 0 }).Max();
            var output = new StringBuilder();
            for (var row = 0; row < matrix.LengthY; row++)
            {
                var first = true;
                for (var col = 0; col < matrix.LengthOfRow(row); col++)
                {
                    output.Append(first
                        ? string.Format(" {0, " + maxLength + "} ", matrix[row, col])
                        : string.Format("| {0, " + maxLength + "} ", matrix[row, col]));
                    first = false;
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        public static string Visualize<T>(this T[,] matrix)
        {
            return Visualize(new MatrixWrapper<T>(matrix));
        }

        public static string Visualize<T>(this T[][] matrix)
        {
            return Visualize(new MatrixWrapper<T>(matrix));
        }

        public static void VisualizeToConsole<T>(this T[,] matrix)
        {
            Console.WriteLine(Visualize(new MatrixWrapper<T>(matrix)));
        }

        public static void VisualizeToConsole<T>(this T[][] matrix)
        {
            Console.WriteLine(Visualize(new MatrixWrapper<T>(matrix)));
        }
        #endregion

        #region Equals

        private static bool Equals<T>(MatrixWrapper<T> matrix1, MatrixWrapper<T> matrix2, Func<T, T, bool> isEqualPredicate = null)
            where T : IEquatable<T>
        {
            if (isEqualPredicate is null)
            {
                isEqualPredicate = (elem1, elem2) => elem1.Equals(elem2);
            }

            if (ReferenceEquals(matrix1, matrix2))
            {
                return true;
            }
            if (matrix1 == null || matrix2 == null)
            {
                return false;
            }
            if (matrix1.LengthY != matrix2.LengthY)
            {
                return false;
            }
            for (var row = 0; row < matrix1.LengthY; row++)
            {
                if (matrix1.LengthOfRow(row) != matrix2.LengthOfRow(row))
                {
                    return false;
                }
                for (var col = 0; col < matrix1.LengthOfRow(row); col++)
                {
                    if (!isEqualPredicate(matrix1[row, col], matrix2[row, col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Equals<T>(this T[,] matrix1, T[,] matrix2, Func<T, T, bool> isEqualPredicate = null)
            where T : IEquatable<T>
        {
            return Equals(new MatrixWrapper<T>(matrix1), new MatrixWrapper<T>(matrix2), isEqualPredicate);
        }

        public static bool Equals<T>(this T[][] matrix1, T[][] matrix2, Func<T, T, bool> isEqualPredicate = null)
            where T : IEquatable<T>
        {
            return Equals(new MatrixWrapper<T>(matrix1), new MatrixWrapper<T>(matrix2), isEqualPredicate);
        }

        public static bool Equals<T>(this T[,] matrix1, T[][] matrix2, Func<T, T, bool> isEqualPredicate = null)
            where T : IEquatable<T>
        {
            return Equals(new MatrixWrapper<T>(matrix1), new MatrixWrapper<T>(matrix2), isEqualPredicate);
        }

        public static bool Equals<T>(this T[][] matrix1, T[,] matrix2, Func<T, T, bool> isEqualPredicate = null)
            where T : IEquatable<T>
        {
            return Equals(new MatrixWrapper<T>(matrix1), new MatrixWrapper<T>(matrix2), isEqualPredicate);
        }
        #endregion

    }
}
