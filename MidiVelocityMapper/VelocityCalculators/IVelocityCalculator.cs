using MidiVelocityMapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidiVelocityMapper.VelocityCalculators
{
    public interface IVelocityCalculator
    {
        public int Calculate(int velocity);
        public IList<VelocityMap> GetMap();
    }
}
