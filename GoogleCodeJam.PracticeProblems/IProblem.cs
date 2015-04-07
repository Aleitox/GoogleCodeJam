using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.PracticeProblems
{
    public interface IProblem
    {
        string FileName{ get; }

        string PrintSolution();
    }
}
