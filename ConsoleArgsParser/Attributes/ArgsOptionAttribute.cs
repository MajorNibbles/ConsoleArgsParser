using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleArgsParser.Attributes
{
    public class ArgsOptionAttribute : Attribute
    {
        public string Name { get; }
        public bool Optional { get; }
        public bool PromptOnNull { get; }

        public ArgsOptionAttribute(string name = null, bool optional = false, bool promptOnNull = true)
        {
            Name = name;
            Optional = optional;
            PromptOnNull = promptOnNull;
        }
    }
}
