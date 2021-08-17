using System;
using ConsoleArgsParser;
using FluentAssertions;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running tests");

            var o = ArgsParser.Parse<TestModel>(args);

            o.stringType.Should().Be("yes");
            o.boolType.Should().BeTrue();
            o.decimalType.Should().Be((decimal) 100.01);
            o.doubleType.Should().Be(10.01);
            o.floatType.Should().Be((float)1000.01);
            o.intType.Should().Be(100);
        }
    }
}
