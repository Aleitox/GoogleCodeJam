using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Interpreter
{
    public class InputProblems<T> where T : new()
    {
        public InputProblems() 
        {
            Problems = new List<T>();
        }

        public int Cases { get; set; }

        public List<T> Problems { get; set; }
    }
}
