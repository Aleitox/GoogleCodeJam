using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Base
{
    public class Problem
    {
        public string FileName { get; set; }

        public virtual string PrintSolution()
        {
            throw new NotImplementedException();
        }
    }
}
