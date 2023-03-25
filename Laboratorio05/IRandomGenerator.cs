using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio05
{
    public interface IRandomGenerator
    {
        private static IRandomGenerator randomGenerator = new RandomGenerator();

        static IRandomGenerator RandomGenerator { get { return randomGenerator; } set { randomGenerator = value; } }

        double Next();
    }
}
