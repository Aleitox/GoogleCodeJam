using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaSigma.RopeIntranet
{
    public class Problem
    {
        public int NumberofWires { get; set; }

        public List<List<int>> Connections { get; set; }


        // Inicio

        public List<int> MagicVector { get; set; }

        public int Solve()
        {
            var vectorLeft = new List<int>();
            var vectorRight = new List<int>();

            foreach (var connection in Connections)
            {
                vectorLeft.Add(connection[0]);
                vectorRight.Add(connection[1]);
            }

            vectorLeft = vectorLeft.OrderBy(x => x).ToList();
            vectorRight = vectorRight.OrderBy(x => x).ToList();

            MagicVector = new List<int>();

            foreach (var leftWindow in vectorLeft)
            {
                var rightWindow = Connections.First(c => c.First() == leftWindow).Last();
                MagicVector.Add(vectorRight.FindIndex(w => w == rightWindow));
            }

            var intersections = 0;

            for (var index = 0; index < MagicVector.Count; index ++)
            {
                if (index < MagicVector[index])
                {
                    intersections += MagicVector[index] - index;
                }
            }

            return intersections;
        }
    }
}
