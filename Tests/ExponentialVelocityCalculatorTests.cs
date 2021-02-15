using MidiVelocityMapper.Models;
using MidiVelocityMapper.VelocityCalculators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class ExponentialVelocityCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LinearMapping_0_to_127_Test()
        {
            var min = 0;
            var max = 127;
            var exp = 1;

            var calculator = new ExponentialVelocityCalculator(min, max, exp);
            var outputMap = calculator.GetMap();

            for (int i = 0; i <= 127; i++)
            {
                Assert.That(outputMap.Single(x => x.In == i).Out, Is.EqualTo(i));
            }
        }

        [Test]
        public void LinearMapping_0_to_100_Test()
        {
            var min = 0;
            var max = 100;
            var exp = 1;

            var calculator = new ExponentialVelocityCalculator(min, max, exp);
            var outputMap = calculator.GetMap();

            for (int i = 0; i <= 127; i++)
            {
                var result = calculator.Calculate(i);
                if (i > max)
                {
                    Assert.That(outputMap.Single(x => x.In == i).Out, Is.EqualTo(127));
                }
                else
                {
                    Assert.That(outputMap.Single(x => x.In == i).Out, Is.EqualTo(Convert.ToInt32(i * 1.27)));
                }
            }
        }

        [Test]
        public void HeavyTouchCurve_0_to_100_Test()
        {
            var min = 0;
            var max = 100;
            var exp = 1.5;

            var calculator = new ExponentialVelocityCalculator(min, max, exp);
            var outputMap = calculator.GetMap();

            var expected = new List<VelocityMap>()
            {
                new VelocityMap(0, 0),
                new VelocityMap(1, 0),
                new VelocityMap(10, 4),
                new VelocityMap(20, 11),
                new VelocityMap(30, 21),
                new VelocityMap(40, 32),
                new VelocityMap(50, 45),
                new VelocityMap(60, 59),
                new VelocityMap(70, 74),
                new VelocityMap(80, 91),
                new VelocityMap(90, 108),
                new VelocityMap(100, 127),
                new VelocityMap(110, 127),
                new VelocityMap(120, 127),
                new VelocityMap(127, 127)
            };

            foreach (var item in expected)
            {
                Assert.That(outputMap.Single(x => x.In == item.In).Out, Is.EqualTo(item.Out));
            }
        }
    }
}