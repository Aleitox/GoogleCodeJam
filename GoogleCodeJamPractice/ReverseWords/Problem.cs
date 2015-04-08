using GoogleCodeJam.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.ReverseWords
{
    public class Problem : Base.Problem
    {
        [InterpreterAttribute(Order = 1)]
        public static int Merigoldo { get; set; }


        [InterpreterAttribute(Order = 1)]
        public string Sentence { get; set; }

        public override string PrintSolution()
        {
            var solution = Solve();
            return solution.ToString();
        }
        
        public Solution Solve()
        {
            var senteceList = Sentence.Split(' ');
            var listaInvertida = senteceList.Reverse().ToList();
            var solution = new Solution(){Sentence = string.Join(" ", listaInvertida)};
            return solution;
        }
    }
}
