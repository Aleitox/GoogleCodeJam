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
                    return new ReverseWords.Problem() { FileName = string.Format("ReverseWords\\B-{0}-practice", sizeEnum.ToString()) };
                case ProblemEnmu.StoreCredit:
                    return new StoreCredit.Problem() { FileName = string.Format("StoreCredit\\A-{0}-practice", sizeEnum.ToString()) };
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum SizeEnum 
    {
        large,
        small
    }

    public enum ProblemEnmu 
    {
        StoreCredit = 1,
        ReverseWords = 2
    }
}
