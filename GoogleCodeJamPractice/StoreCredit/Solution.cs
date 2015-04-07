using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.StoreCredit
{
    public class Solution
    {
        public int Credit { get; set; }
        public Item First { get; set; }
        public Item Second { get; set; }
        public bool IsSolution()
        {
            return Credit == First.Value + Second.Value;
        }

        public override string ToString()
        {
            // falta "Case #{0}:"
            return string.Format(" {0} {1}", First.Index, Second.Index);
        }
    }
    public class Item
    {
        public int Index { get; set; }
        public int Value { get; set; }
    }

}
