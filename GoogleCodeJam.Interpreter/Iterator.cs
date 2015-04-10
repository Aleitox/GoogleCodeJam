using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Interpreter
{
    public abstract class Iterator<T>
    {
        //public abstract T First();
        //public abstract T Next();
        public abstract T Read();
        public abstract List<T> ReadLine();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }


    public class ListListIterator<T> : Iterator<T>
    {
        private List<List<T>> _input { get; set; }
        private int _row = 0;
        private int _offSet = 0;
        private bool _done;

        public ListListIterator(List<List<T>> input) 
        {
            _input = input;
            _done = input.Count <= _row;
        }

        //public override T First()
        //{
        //    return _input[0][0];
        //}

        //public override T Next()
        //{
        //    var value = default(T);
        //    if (_input[_row].Count - 1 < _offSet)
        //    {
        //        _offSet++;
        //        value = CurrentItem();
        //    }
        //    else
        //    {
        //        _row++;
        //        _offSet = 0;
        //        if (_input.Count <= _row)
        //            _done = true;
        //        else
        //            value = CurrentItem();
        //    }
        //    return value;
        //}        

        public override bool IsDone()
        {
            return _done;
        }

        public override T CurrentItem()
        {
            return _input[_row][_offSet];
        }

        public override T Read()
        {
            if (_done) return default(T);
            var value = CurrentItem();
            if (_offSet < _input[_row].Count - 1)
                _offSet++;
            else
            {
                _row++;
                _offSet = 0;
                if (_input.Count <= _row)
                    _done = true;
            }
            return value;
        }

        public override List<T> ReadLine()
        {
            if (_done) return null;
            var value = _input[_row];
            _row++;
            _offSet = 0;
            if (_input.Count <= _row)
                _done = true;            
            return value;
        }
    }
}
