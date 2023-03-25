using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio05
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random random;

        internal RandomGenerator() 
        {
            random = new Random();
        }

        public double Next()
        {
            return random.NextInt64(-100, 101) / 100.0;
        }
    }
}
