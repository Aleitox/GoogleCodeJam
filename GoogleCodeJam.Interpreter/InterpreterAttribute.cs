using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Interpreter
{
    public class InterpreterAttribute : Attribute
    {
        public InterpreterAttribute()
        {
            InitializeMethod = string.Empty;
        }

        public int Order { get; set; }

        public string[] ItitializeAttibutes { get; set; }

        public string InitializeMethod { get; set; }
    }    
}
