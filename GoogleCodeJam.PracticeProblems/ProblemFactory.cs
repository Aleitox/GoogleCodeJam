using GoogleCodeJamPractice.StoreCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.PracticeProblems
{
    public static class ProblemFactory
    {
        public static IProblem Get()
        {
            return new Problem();
        }
    }
}
