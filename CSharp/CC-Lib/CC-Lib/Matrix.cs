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

        public static T[,] Copy<T>(this T[,] matrix)
        {
            var copied = new T[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    copied[i, j] = matrix[i, j];
                }
            }

            return copied;
        }

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

        public static string Visualize<T>(this T[,] matrix)
        {
            int maxLength = (from T value in matrix select value.ToString() into str select str.Length).Concat(new[] {0}).Max();
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
    }
}
