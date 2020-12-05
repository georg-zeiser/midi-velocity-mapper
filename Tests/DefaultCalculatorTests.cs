using MidiVelocityMapper.Models;
using MidiVelocityMapper.VelocityCalculators;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class DefaultCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LinearMappingTest()
        {
            var inputMap = new List<VelocityMap>()
            {
                new VelocityMap(0, 0),
                new VelocityMap(127, 127)
            };

            var calculator = new DefaultVelocityCalculator(inputMap);
            var outputMap = calculator.GetMap();

            for (int i = 0; i <= 127; i++)
            {
                var result = calculator.Calculate(i);
                Assert.That(result, Is.EqualTo(i));
                Assert.That(outputMap.Single(x => x.In == i).Out, Is.EqualTo(i));
            }
        }

        [Test]
        public void MultiplePointsMappingTest()
        {
            var inputMap = new List<VelocityMap>()
            {
                new VelocityMap(1, 5),
                new VelocityMap(25, 25),
                new VelocityMap(60, 64),
                new VelocityMap(100, 127)
            };

            var expected = new List<VelocityMap>()
            {
                new VelocityMap(0, 0),
                new VelocityMap(1, 5),
                new VelocityMap(25, 25),
                new VelocityMap(60, 64),
                new VelocityMap(64, 70),
                new VelocityMap(100, 127),
                new VelocityMap(127, 127)
            };

            var calculator = new DefaultVelocityCalculator(inputMap);
            var outputMap = calculator.GetMap();

            foreach (var item in expected)
            {
                var result = calculator.Calculate(item.In);
                Assert.That(result, Is.EqualTo(item.Out));
                Assert.That(outputMap.Single(x => x.In == item.In).Out, Is.EqualTo(item.Out));
            }
        }
    }
}