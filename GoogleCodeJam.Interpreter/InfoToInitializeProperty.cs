using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.Interpreter
{
    public class InfoToInitializeProperty
    {
        public int Order { get; set; }

        public string PropertyName { get; set; }

        public MethodInfo MethodForInitialize { get; set; }

        public List<ItitializeAttibute> ItitializeAttibutes { get; set; }
    }

    public class ItitializeAttibute
    {
        public string OtherPropertyName { get; set; }

        public PropertyAttribute ThisPropertyAttribute { get; set; }
    }

    public enum PropertyAttribute
    {
        Length,
        Lines,
        Rows,
        Columns
    }
}
