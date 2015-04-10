using System;
using System.Collections.Generic;
using GoogleCodeJam.Interpreter;
using GoogleCodeJam.Model;

namespace GoogleCodeJam.Rotate
{
    public class Problem : Base.Problem
    {
        public int MatrixSize { get; set; }

        [Interpreter(Order = 1)]
        public int N { get; set; }

        [Interpreter(Order = 2)]
        public int K { get; set; }

        [Interpreter(Order = 3, ItitializeAttibutes = new[] { "N", "Rows", "N", "Columns"})]
        public Matrix<string> Matrix { get; set; }

        public const string Red = "R";
        public const string Blue = "B";
        public const string Empty = ".";

        public override string PrintSolution()
        { 
            Matrix.RotateClockwise();
            for (var column = 0; column < N; column++)
                    ApplyGravity(column);

            var playerRedWon = PlayerWon(Red);
            var playerBlueWon = PlayerWon(Blue);
            if (playerRedWon && playerBlueWon)
                return "Both";
            if (!playerRedWon && playerBlueWon)
                return Blue;
            if (playerRedWon)
                return Red;
            return "Neither";

        }

        private void ApplyGravity(int column)
        {
            var tokkens = new List<string>();
            for (var row = 0; row < N; row++)
            {
                var token = Matrix.Get(row, column);
                if (token != Empty)
                    tokkens.Add(token);
            }

            for (var row = 0; row < N; row++)
                Matrix.Set(row, column, row < tokkens.Count ? tokkens[row] : Empty);


        }

        public bool PlayerWon(string player)
        {
            if (K == 0) return true;
            var position = new Position() {Column = 0, Row = 0};
            var directions = new List<DirectionEnum>() {DirectionEnum.Vertical, DirectionEnum.Diagonal, DirectionEnum.Horizontal };
            return StartFromPosition(player, position, directions);
        }

        private bool StartFromPosition(string player, Position position, List<DirectionEnum> directions)
        {
            if (OutOfRange(position)) return false;

            foreach (var direction in directions)
            {
                if (IsSolution(player, position, direction, 0))
                    return true;
            }

            if (StartFromPosition(player, GetNextPosition(position, DirectionEnum.Vertical), directions))
                return true;

            if (StartFromPosition(player, GetNextPosition(position, DirectionEnum.Horizontal), directions))
                return true;

            return false;
        }

        private bool IsSolution(string player, Position position, DirectionEnum direction, int checkedItems)
        {
            if (checkedItems >= K) return true;

            if (OutOfRange(position)) return false;

            var positionValue = Matrix.Get(position.Row, position.Column);

            if (positionValue != player) return false;
            
            checkedItems++;

            return IsSolution(player, GetNextPosition(position, direction), direction, checkedItems);

        }

        private Position GetNextPosition(Position position,DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Vertical: return new Position() { Row = position.Row + 1, Column = position.Column};
                case DirectionEnum.Diagonal: return new Position() { Row = position.Row + 1, Column = position.Column + 1 };
                case DirectionEnum.Horizontal: return new Position() { Row = position.Row, Column = position.Column + 1 };
                default: throw new Exception("Invalid direction");
            }
        }

        private bool OutOfRange(Position position)
        {
            return position.Row >= K || position.Column >= K;
        }
    }

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

    }

    public enum DirectionEnum
    {
        Vertical,
        Diagonal,
        Horizontal
    }
}
