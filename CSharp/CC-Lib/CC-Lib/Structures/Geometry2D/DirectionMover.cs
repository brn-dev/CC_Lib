namespace CC_Lib.Structures.Geometry2D
{

    public class DirectionMover
    {
        public DirectionMover(Vector2 position, Direction direction = Direction.Up)
        {
            Position = position;
            Direction = direction;
        }

        public Vector2 Position { get; set; }
        public Direction Direction { get; set; }

        public void Forward()
        {
            Position += Direction.GetVector();
        }

        public void Backward()
        {
            Position -= Direction.GetVector();
        }

        public void TurnClockwise()
        {
            Direction = Direction.TurnClockwise();
        }

        public void TurnClockwise(int times)
        {
            Direction = Direction.TurnClockwise(times);
        }

        public void TurnCounterClockwise()
        {
            Direction = Direction.TurnCounterClockwise();
        }

        public void TurnCounterClockwise(int times)
        {
            Direction = Direction.TurnCounterClockwise(times);
        }
    }
}
