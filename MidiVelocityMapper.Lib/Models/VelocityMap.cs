using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidiVelocityMapper.Lib.Models
{
    public class VelocityMap
    {
        public VelocityMap()
        {
        }

        public VelocityMap(int @in, int @out)
        {
            In = @in;
            Out = @out;
        }

        public int In { get; set; }
        public int Out { get; set; }
    }
}
