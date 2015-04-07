using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaSigma.MinimumScalarProduct
{
    public class Problem
    {
        public int VectorSize { get; set; }

        public List<int> FirstVector { get; set; }

        public List<int> SecondVector { get; set; }

        public int Solve()
        {
            var solution = ScalarProduct(FirstVector.OrderBy(x => x).ToList(), SecondVector.OrderByDescending(x => x).ToList());
            return solution;
        }

        public int ScalarProduct(List<int> first, List<int> second)
        {
            if(first.Count != second.Count)
                throw new Exception("Listas de tamaño diferentes.");

            return first.Select((t, index) => t*second[index]).Sum();
        }
    }
}
