using System;
using System.Linq;
using System.Text;
// ReSharper disable FormatStringProblem

namespace CC_Lib
{
    /// <summary>
    /// Extension methods for 2 dimensional arrays.
    /// All functions assume that the first index is the y coordinate (the row - starting from the top) and the second index is the x coordinate (the column) 
    /// </summary>
    public static class Matrix
    {

        #region GetColumn & GetRow

        public static T[] GetColumn<T>(this T[,] matrix, int colIndex)
        {
            if (colIndex < 0 || colIndex >= matrix.GetLength(1))
            {
                throw new IndexOutOfRangeException($"rowIndex is out of range. 0 <= i <= {matrix.GetLength(0) - 1}, but rowIndex was {colIndex}");
            }

            var col = new T[matrix.GetLength(0)];
            for (int i = 0; i < col.Length; i++)
            {
                col[i] = matrix[i, colIndex];
            }
            return col;
        }

        public static T[] GetColumn<T>(this T[][] matrix, int colIndex)
        {
            if (colIndex < 0)
            {
                throw new IndexOutOfRangeException($"colIndex may not be smaller than 0! (colIndex is {colIndex})");
            }

            var col = new T[matrix.Length];
            for (int i = 0; i < col.Length; i++)
            {
                if (colIndex >= matrix[i].Length)
                {
                    throw new IndexOutOfRangeException($"Subarray of matrix at index {i} isn't long enough to get the column index {colIndex} (subarray is {matrix[i].Length} elements long)");
                }
                col[i] = matrix[i][colIndex];
            }
            return col;
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0))
            {
                throw new IndexOutOfRangeException($"rowIndex is out of range. 0 <= i <= {matrix.GetLength(1) - 1}, but rowIndex was {rowIndex}");
            }

            var row = new T[matrix.GetLength(1)];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = matrix[rowIndex, i];
            }
            return row;
        }

        public static T[] GetRow<T>(this T[][] matrix, int rowIndex)
        {
            return matrix[rowIndex];
        }

        #endregion

        #region Copy
        public static T[,] Copy<T>(this T[,] matrix)
        {
            var copy = new T[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        public static T[][] Copy<T>(this T[][] matrix)
        {
            var copy = new T[matrix.Length][];

            for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
            {
                copy[rowIndex] = new T[matrix[rowIndex].Length];
                for (int columnIndex = 0; columnIndex < copy[rowIndex].Length; columnIndex++)
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

        public static T[] ToFlat<T>(this T[,] matrix)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            var flatArr = new T[width * height];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    flatArr[width * row + column] = matrix[row, column];
                }
            }
            return flatArr;
        }

        public static T[] ToFlat<T>(this T[][] matrix)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            var flatArr = new T[width * height];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    flatArr[width * row + column] = matrix[row][column];
                }
            }
            return flatArr;
        }

        public static T[][] ToNested<T>(this T[,] matrix)
        {
            int height = matrix.GetLength(0);
            var nestedArr = new T[height][];
            for (int row = 0; row < height; row++)
            {
                nestedArr[row] = matrix.GetRow(row);
            }
            return nestedArr;
        }

        public static T[,] To2DArray<T>(this T[][] nestedArray)
        {
            int height = nestedArray.Length;
            if (nestedArray.Select(row => row.Length).Distinct().Count() > 1)
            {
                throw new ArgumentException("Subarrays of nestedArray must be same size!");
            }
            int width = nestedArray[0].Length;
            var array2D = new T[height, width];
            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (int colIndex = 0; colIndex < width; colIndex++)
                {
                    array2D[rowIndex, colIndex] = nestedArray[colIndex][rowIndex];
                }
            }
            return array2D;
        }

        #endregion

        #region ForEach
        public static void ForEach<T>(this T[,] matrix, Action<T> action)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    action(matrix[row, column]);
                }
            }
        }

        public static bool ForEach<T>(this T[,] matrix, Func<T, bool> func)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (!func(matrix[row, column]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        public static void ForEach<T>(this T[][] matrix, Action<T> action)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    action(matrix[row][column]);
                }
            }
        }

        public static bool ForEach<T>(this T[][] matrix, Func<T, bool> func)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (!func(matrix[row][column]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Enumerate

        public static void Enumerate<T>(this T[,] matrix, Action<int, int, T> action)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    action(row, column, matrix[row, column]);
                }
            }
        }

        public static bool Enumerate<T>(this T[,] matrix, Func<int, int, T, bool> func)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (!func(row, column, matrix[row, column]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Enumerate<T>(this T[][] matrix, Action<int, int, T> action)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    action(row, column, matrix[row][column]);
                }
            }
        }

        public static bool Enumerate<T>(this T[][] matrix, Func<int, int, T, bool> action)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (!action(row, column, matrix[row][column]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region AssignEach

        public static void AssignEach<T>(this T[,] matrix, Func<T, T> assignmentFunc)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    matrix[row, column] = assignmentFunc(matrix[row, column]);
                }
            }
        }

        public static void AssignEach<T>(this T[][] matrix, Func<T, T> assignmentFunc)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    matrix[row][column] = assignmentFunc(matrix[row][column]);
                }
            }
        }

        public static void AssignEach<T>(this T[,] matrix, Func<int, int, T, T> assignmentFunc)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    matrix[row, column] = assignmentFunc(row, column, matrix[row, column]);
                }
            }
        }

        public static void AssignEach<T>(this T[][] matrix, Func<int, int, T, T> assignmentFunc)
        {
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    matrix[row][column] = assignmentFunc(row, column, matrix[row][column]);
                }
            }
        }
        #endregion

        #region Visualize
        public static string Visualize<T>(this T[,] matrix)
        {
            int maxLength = (from T value in matrix select value.ToString() into str select str.Length).Concat(new[] { 0 }).Max();
            var output = new StringBuilder();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                bool first = true;
                for (int col = 0; col < matrix.GetLength(1); col++)
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

        public static void VisualizeToConsole<T>(this T[,] matrix)
        {
            Console.WriteLine(Visualize(matrix));
        }
        #endregion

        #region Equals
        public static bool Equals<T>(T[,] matrix1, T[,] matrix2)
            where T : IEquatable<T>
        {
            if (matrix1 == matrix2)
            {
                return true;
            }
            if (matrix1 == null || matrix2 == null)
            {
                return false;
            }
            if (matrix1.GetLength(0) != matrix2.GetLength(0))
            {
                return false;
            }
            if (matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                return false;
            }
            int height = matrix1.GetLength(0);
            int width = matrix1.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (!matrix1[row, col].Equals(matrix2[row, col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Equals<T>(T[][] matrix1, T[][] matrix2)
            where T : IEquatable<T>
        {
            if (matrix1 == matrix2)
            {
                return true;
            }
            if (matrix1 == null || matrix2 == null)
            {
                return false;
            }
            if (matrix1.Length != matrix2.Length)
            {
                return false;
            }
            for (int row = 0; row < matrix1.Length; row++)
            {
                if (matrix1[row].Length != matrix2[row].Length)
                {
                    return false;
                }
                for (int col = 0; col < matrix1[row].Length; col++)
                {
                    if (!matrix1[row][col].Equals(matrix2[row][col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Equals<T>(T[,] matrix1, T[,] matrix2, Func<T, T, bool> isEqualPredicate)
        {
            if (matrix1 == matrix2)
            {
                return true;
            }
            if (matrix1 == null || matrix2 == null)
            {
                return false;
            }
            if (matrix1.GetLength(0) != matrix2.GetLength(0))
            {
                return false;
            }
            if (matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                return false;
            }
            int height = matrix1.GetLength(0);
            int width = matrix1.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (!isEqualPredicate(matrix1[row, col], matrix2[row, col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Equals<T>(T[][] matrix1, T[][] matrix2, Func<T, T, bool> isEqualPredicate)
        {
            if (matrix1 == matrix2)
            {
                return true;
            }
            if (matrix1 == null || matrix2 == null)
            {
                return false;
            }
            if (matrix1.Length != matrix2.Length)
            {
                return false;
            }
            for (int row = 0; row < matrix1.Length; row++)
            {
                if (matrix1[row].Length != matrix2[row].Length)
                {
                    return false;
                }
                for (int col = 0; col < matrix1[row].Length; col++)
                {
                    if (!isEqualPredicate(matrix1[row][col], matrix2[row][col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

    }
}
