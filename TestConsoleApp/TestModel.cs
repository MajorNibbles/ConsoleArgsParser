using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleArgsParser.Attributes;

namespace TestConsoleApp
{
    public class TestModel
    {
        public string stringType { get; set; }
        public int intType { get; set; }
        public decimal decimalType { get; set; }
        public double doubleType { get; set; }
        public float floatType { get; set; }
        public bool boolType { get; set; }
        //[ArgsOption(optional: false, promptOnNull: false)]
        //public string ShouldThrowWhenNotOptional { get; set; }
        [ArgsOption(optional: false, promptOnNull: true)]
        public string ShouldPrompt { get; set; }
    }
}
