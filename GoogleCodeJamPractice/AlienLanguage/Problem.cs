using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeltaSigma.AlienLanguage
{
    public class Problem
    {
        //L
        public int WordsLength { get; set; }
        //D
        public int NumberOfWordsInLanguage { get; set; }

        public List<string> Words { get; set; }


        // Problem particular:


        public string PosibleMessage { get; set; }


        public int Solve()
        {
            var regex = new Regex(PosibleMessageRegex);
            return Words.Count(regex.IsMatch);
        }

        public string PosibleMessageRegex { get; set; }

        public void SetPosibleMessageRegex()
        {
            const string orChar = "|";
            var insideParenthesis = false;
            var posibleMessageRegex = string.Empty;

            for (var index = 0; index < PosibleMessage.Count(); index++)
            {
                posibleMessageRegex += PosibleMessage[index];
                if (insideParenthesis)
                {
                    if (PosibleMessage[index] == ')')
                        insideParenthesis = false;
                    else
                        posibleMessageRegex += orChar;
                }
                else
                    if (PosibleMessage[index] == '(')
                        insideParenthesis = true;
            }

            PosibleMessageRegex = posibleMessageRegex;
        }


    }
}
