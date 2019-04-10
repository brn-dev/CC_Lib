using System.Linq;

namespace Test
{
    internal class EqualityHelper
    {
        public static bool ArrayEquals<T>(T[] ar1, T[] ar2)
        {
            var q = from a in ar1
                join b in ar2 on a equals b
                select a;

            bool equals = ar1.Length == ar2.Length && q.Count() == ar1.Length;
            return equals;
        }

        public static bool MatrixEquals<T>(T[,] matrix1, T[,] matrix2)
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

        public static bool Array2DEquals<T>(T[][] matrix1, T[][] matrix2)
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
    }
}
