using GoogleCodeJam.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJamPractice
{
    public class Problem
    {
        [InterpreterAttribute(Order = 1)]
        public int Credit { get; set; }

        [InterpreterAttribute(Order = 2)]
        public int ItemsCount { get; set; }

        [InterpreterAttribute(Order = 3, ItitializeAttibutes = new[] { "ItemsCount", "Length" })]
        public List<int> Items { get; set; }

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
    }
}
