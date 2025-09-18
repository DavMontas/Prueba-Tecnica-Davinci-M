using core.application.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.Common
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _rnd = new();
        public int NextInt(int minInclusive, int maxInclusive)
        {
            return _rnd.Next(minInclusive, maxInclusive + 1);
        }
    }
}
