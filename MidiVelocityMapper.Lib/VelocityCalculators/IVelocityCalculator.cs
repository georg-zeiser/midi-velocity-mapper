using MidiVelocityMapper.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidiVelocityMapper.Lib.VelocityCalculators
{
    public interface IVelocityCalculator
    {
        public int Calculate(int velocity);
        public IList<VelocityMap> GetMap();
    }
}
