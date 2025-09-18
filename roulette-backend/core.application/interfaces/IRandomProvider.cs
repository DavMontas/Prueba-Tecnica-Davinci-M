using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.interfaces
{
    public interface IRandomProvider
    {
        int NextInt(int minInclusive, int maxInclusive);
    }
}
