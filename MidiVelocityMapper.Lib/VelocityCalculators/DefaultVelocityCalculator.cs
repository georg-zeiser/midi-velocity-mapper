using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MidiVelocityMapper.Lib.Models;

namespace MidiVelocityMapper.Lib.VelocityCalculators
{
    public class DefaultVelocityCalculator : IVelocityCalculator
    {
        private readonly IList<VelocityMap> _map;

        public DefaultVelocityCalculator(IList<VelocityMap> map)
        {
            _map = CleanUp(map);
        }

        public int Calculate(int velocity)
        {
            var found = _map.SingleOrDefault(x => x.In == velocity);
            if (found != null)
            {
                return found.Out;
            }

            var prev = _map.Last(x => x.In < velocity);
            var next = _map.First(x => x.In > velocity);

            double minIn = prev.In;
            double minOut = prev.Out;
            double maxIn = next.In;
            double maxOut = next.Out;


            //some formulas :-)
            //linear function, going through 2 points:
            //    y = mx + b
            //    m = (y2 - y1) / (x2 - x1)

            double y2 = maxOut;
            double y1 = minOut;
            double x1 = minIn;
            double x2 = maxIn;

            double m = (y2 - y1) / (x2 - x1);

            double x = maxIn;
            double y = maxOut;
            double b = y - m * x;

            var result = velocity * m + b;
            result = Math.Max(result, minOut);
            result = Math.Min(result, maxOut);

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

        private IList<VelocityMap> CleanUp(IList<VelocityMap> map)
        {
            if (map == null)
            {
                map = new List<VelocityMap>();
            }

            if (!map.Any(x => x.In == 0))
            {
                map.Add(new VelocityMap(0, 0));
            }

            if (!map.Any(x => x.In == 127))
            {
                map.Add(new VelocityMap(127, 127));
            }

            var invalid = false;
            if (map.Any(x => x.In < 0
             || x.In > 127
             || x.Out < 0
             || x.Out > 127)
                )
            {
                invalid = true;
            }

            for (int i = 0; i <= 127; i++)
            {
                if (map.Count(x => x.In == i) > 1)
                {
                    invalid = true;
                    break;
                }
            }

            if (invalid)
            {
                throw new ArgumentException("velocity settings are invalid");
            }

            return map.OrderBy(x => x.In).ToList();
        }
    }
}
