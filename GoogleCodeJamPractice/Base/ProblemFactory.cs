using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Base
{
    public static class ProblemFactory
    {
        public static Problem Get(ProblemEnmu problemEnmu, SizeEnum sizeEnum)
        {
            switch (problemEnmu) 
            {
                case ProblemEnmu.ReverseWords:
                    return new ReverseWords.Problem() { FileName = string.Format("ReverseWords\\B-{0}-practice", sizeEnum) };
                case ProblemEnmu.StoreCredit:
                    return new StoreCredit.Problem() { FileName = string.Format("StoreCredit\\A-{0}-practice", sizeEnum) };
                case ProblemEnmu.Rotate:
                    return new Rotate.Problem() { FileName = string.Format("Rotate\\A-{0}-practice", sizeEnum) };
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum SizeEnum 
    {
        Large,
        Small
    }

    public enum ProblemEnmu 
    {
        StoreCredit = 1,
        ReverseWords = 2,
        Rotate = 3
    }
}
