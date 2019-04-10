using CC_Lib;
using CC_Lib.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class MatrixTest
    {
        private int[,] _testMatrix;
        private int[,] _emptyMatrix;

        private int[][] _testArray2D;
        private int[][] _emptyArray2D;

        [TestInitialize]
        public void Before()
        {
            _testMatrix = new[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12}
            };

            _testArray2D = new[]
            {
                new[] {1, 2, 3, 4},
                new[] {5, 6, 7, 8},
                new[] {9, 10, 11, 12}
            };

            _emptyMatrix = new int[0,0];

            _emptyArray2D = new int[0][];
        }

        [TestMethod]
        public void MatrixGetColumnTest()
        {
            Assert.IsTrue(Equality.ArrayEquals(new[] { 1, 5, 9 }, _testMatrix.GetColumn(0)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 2, 6, 10 }, _testMatrix.GetColumn(1)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 3, 7, 11 }, _testMatrix.GetColumn(2)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 4, 8, 12 }, _testMatrix.GetColumn(3)));
        }

        [TestMethod]
        public void Array2DGetColumnTest()
        {
            Assert.IsTrue(Equality.ArrayEquals(new[] { 1, 5, 9 }, _testArray2D.GetColumn(0)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 2, 6, 10 }, _testArray2D.GetColumn(1)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 3, 7, 11 }, _testArray2D.GetColumn(2)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 4, 8, 12 }, _testArray2D.GetColumn(3)));
        }

        [TestMethod]
        public void GetRowTest()
        {
            Assert.IsTrue(Equality.ArrayEquals(new[] { 1, 2, 3, 4 }, _testMatrix.GetRow(0)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 5, 6, 7, 8 }, _testMatrix.GetRow(1)));
            Assert.IsTrue(Equality.ArrayEquals(new[] { 9, 10, 11, 12 }, _testMatrix.GetRow(2)));
        }

        [TestMethod]
        public void MatrixCopyTest()
        {
            var copied = _testMatrix.Copy();

            _emptyMatrix.Copy(); // should work with empty matrix

            Assert.IsFalse(ReferenceEquals(copied, _testMatrix), "Copied and base matrix shouldn't be same object");
            Assert.IsTrue(Equality.MatrixEquals(copied, _testMatrix), "Copied and base matrix should be equal");
        }

        [TestMethod]
        public void Array2DCopyTest()
        {
            var copied = _testArray2D.Copy();

            _emptyArray2D.Copy(); // should work with empty 2D-array

            Assert.IsFalse(ReferenceEquals(copied, _testArray2D), "Copied and base 2D-array shouldn't be same object");
            Assert.IsTrue(Equality.Array2DEquals(copied, _testArray2D), "Copied and base 2D-array should be equal");
        }

        [TestMethod]
        public void MirrorVerticallyTest()
        {
            var expectedMirror = new[,]
            {
                {4, 3, 2, 1},
                {8, 7, 6, 5},
                {12, 11, 10, 9}
            };

            var mirrored = _testMatrix.MirrorVertically();

            _emptyMatrix.MirrorVertically(); // should work with empty matrix

            Assert.IsFalse(ReferenceEquals(expectedMirror, mirrored), "Mirrored and base matrix shouldn't be same object");
            Assert.IsTrue(Equality.MatrixEquals(expectedMirror, mirrored), "Should vertically mirror correctly");
        }

        [TestMethod]
        public void MirrorHorizontallyTest()
        {
            var expectedMirror = new[,]
            {
                {9, 10, 11, 12},
                {5, 6, 7, 8},
                {1, 2, 3, 4}
            };

            var mirrored = _testMatrix.MirrorHorizontally();

            _emptyMatrix.MirrorHorizontally(); // should work with empty matrix

            Assert.IsFalse(ReferenceEquals(expectedMirror, mirrored), "Mirrored and base matrix shouldn't be same object");
            Assert.IsTrue(Equality.MatrixEquals(expectedMirror, mirrored), "Should horizontally mirror correctly");
        }

        [TestMethod]
        public void RotateClockwiseTest()
        {
            var expected = new[,]
            {
                {9, 5, 1},
                {10, 6, 2},
                {11, 7, 3},
                {12, 8, 4}
            };

            var rotated = _testMatrix.RotateClockwise();

            _emptyMatrix.RotateClockwise();

            Assert.IsFalse(ReferenceEquals(_testMatrix, rotated), "Rotated and base matrix shouldn't be same object");
            Assert.IsTrue(Equality.MatrixEquals(expected, rotated), "Should clockwise rotate correctly");
        }

        [TestMethod]
        public void RotateCounterClockwiseTest()
        {

            var expected = new[,]
            {
                {4, 8, 12},
                {3, 7, 11},
                {2, 6, 10},
                {1, 5, 9}
            };

            var rotated = _testMatrix.RotateCounterClockwise();

            _emptyMatrix.RotateCounterClockwise();

            Assert.IsFalse(ReferenceEquals(_testMatrix, rotated), "Rotated and base matrix shouldn't be same object");
            Assert.IsTrue(Equality.MatrixEquals(expected, rotated), "Should counter-clockwise rotate correctly");
        }

    }
}
