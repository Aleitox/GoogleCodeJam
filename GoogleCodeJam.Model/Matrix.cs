using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Model
{
    public class Matrix<T> //where T : class
    {
        public Matrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _matrix = new T[rows, columns];
            CurrentPosition = SideEnum.Down;
        }

        private T[,] _matrix { get; set; }

        private int _rows { get; set; }
        private int _columns { get; set; }

        private List<string> __PrityPrint {
            get
            {
                var ret = new List<string>();

                for (var r = 0; r < GetRows(); r++)
                {
                    var aRow = string.Empty;
                    for (var c = 0; c < GetColumns(); c++)
                    {
                        aRow += Get(r, c) + " ";
                    }
                    ret.Add(aRow);
                }

                return ret;
            }
        }

        private SideEnum CurrentPosition { get; set; }

        public int GetColumns()
        {
            switch (CurrentPosition)
            {
                case SideEnum.Down:
                case SideEnum.Up:
                    return _columns;
                case SideEnum.Right:
                case SideEnum.Left:
                    return _rows;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int GetRows()
        {
            switch (CurrentPosition)
            {
                case SideEnum.Down:
                case SideEnum.Up:
                    return _rows;
                case SideEnum.Right:
                case SideEnum.Left:
                    return _columns;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RotateClockwise() 
        {
            CurrentPosition = (SideEnum)(((int)CurrentPosition + 4 + 1) % 4);
        }

        public void RotateCounterClockwise()
        {
            CurrentPosition = (SideEnum)(((int)CurrentPosition + 4 - 1) % 4);
        }

        public T Get(int aRow, int aColumn)
        {
            switch (CurrentPosition)
            {
                case SideEnum.Down:
                    return _matrix[aRow, aColumn];
                case SideEnum.Right:
                    return _matrix[_columns - 1 - aColumn, aRow];
                    break;
                case SideEnum.Up:
                    return _matrix[_columns - 1 - aColumn, _rows - 1 - aRow];
                    break;
                case SideEnum.Left:
                    return _matrix[aColumn, _rows - 1 - aRow];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Set(int aRow, int aColumn, T aValue)
        {
            switch (CurrentPosition)
            {
                case SideEnum.Down:
                    _matrix[aRow, aColumn] = aValue;
                    break;
                case SideEnum.Right:
                    _matrix[_columns - 1 - aColumn, aRow] = aValue;
                    break;
                case SideEnum.Up:
                    _matrix[_columns - 1 - aColumn, _rows - 1 - aRow] = aValue;
                    break;
                case SideEnum.Left:
                    _matrix[aColumn, _rows - 1 - aRow] = aValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum SideEnum 
        {
            Down = 0,
            Right = 1,
            Up = 2,
            Left = 3
        }
    }
}
