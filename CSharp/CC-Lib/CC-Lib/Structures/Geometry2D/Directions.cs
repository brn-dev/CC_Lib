namespace CC_Lib.Structures.Geometry2D
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class Directions
    {
        public static Vector2[] Vectors { get; } = {Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left};

        public static Vector2 GetVector(this Direction direction)
        {
            return Vectors[(int) direction];
        }

        public static Direction TurnClockwise(this Direction direction)
        {
            return (Direction)(((int)direction + 1) % 4);
        }

        public static Direction TurnClockwise(this Direction direction, int times)
        {
            return (Direction)(((int)direction + times) % 4);
        }

        public static Direction TurnCounterClockwise(this Direction direction)
        {
            return (Direction)(((int)direction + 3) % 4);
        }

        public static Direction TurnCounterClockwise(this Direction direction, int times)
        {
            return (Direction)(((int)direction + 3 * times) % 4);
        }

        public static Direction Inverse(this Direction direction)
        {
            return (Direction)(((int) direction + 2) % 4);
        }
    }
}
