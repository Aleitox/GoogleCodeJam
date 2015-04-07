using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Interpreter
{
    public class OutputSolutions<T> where T : new()
    {
        public OutputSolutions() 
        {
            Solutions = new List<T>();
        }

        public int Cases { get { return Solutions.Count; } }

        public List<T> Solutions { get; set; }

        public override string ToString()
        {
            var ret = string.Empty;
            for (var index = 0; index < Cases; index++)
            {
                
            }

            return ret;
        }
    }
}
