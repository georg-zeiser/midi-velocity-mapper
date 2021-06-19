using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MidiVelocityMapper.Lib.Models;

namespace MidiVelocityMapper.Lib.VelocityCalculators
{
    public class ExponentialVelocityCalculator : IVelocityCalculator
    {
        private readonly double _min;
        private readonly double _max;
        private readonly double _exponent;

        public ExponentialVelocityCalculator(int max, double exponent)
        {
            _min = 0;
            _max = max;
            _exponent = exponent;
        }

        public int Calculate(int velocity)
        {
            if (velocity == 0)
            {
                return 0;
            }

            var result = 127 * Math.Pow((velocity - _min) / (_max - _min), _exponent);
            if (result > 127)
            {
                result = 127;
            }
            else if (result < 1 || Double.IsNaN(result))
            {
                result = 1;
            }

            return Convert.ToInt32(result);
        }

        public IList<VelocityMap> GetMap()
        {
            var result = new List<VelocityMap>();
            for (int i = 0; i <= 127; i++)
            {
                result.Add(new VelocityMap(i, Calculate(i)));
            }
            return result;
        }
    }
}
