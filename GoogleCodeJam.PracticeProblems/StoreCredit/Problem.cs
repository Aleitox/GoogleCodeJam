using GoogleCodeJam.Interpreter;
using GoogleCodeJam.PracticeProblems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJamPractice.StoreCredit
{
    public class Problem : IProblem
    {
        [InterpreterAttribute(Order = 1)]
        public int Credit { get; set; }

        [InterpreterAttribute(Order = 2)]
        public int ItemsCount { get; set; }

        [InterpreterAttribute(Order = 3, ItitializeAttibutes = new[] { "ItemsCount", "Length" })]
        public List<int> Items { get; set; }

        public string FileName { get; protected set; }

        public Problem() { }

        public Problem(int credit, int itemsCount, List<int> items)
        {
            Credit = credit;
            ItemsCount = itemsCount;
            Items = items;
        }

        public Solution Solve()
        {
            var posibleSolution = new Solution() { Credit = Credit};
            for(var index = 0; index < Items.Count; index ++)
            {
                posibleSolution.First = new Item() { Index = index + 1, Value = Items[index] };
                for (var secondIndex = index + 1; secondIndex < Items.Count; secondIndex++)
                {
                    posibleSolution.Second = new Item() { Index = index + 1, Value = Items[index] };
                    if (posibleSolution.IsSolution())
                        return posibleSolution;
                }
            }
            return posibleSolution;
        }

        public string PrintSolution()
        {
            throw new NotImplementedException();
        }

    }
}
