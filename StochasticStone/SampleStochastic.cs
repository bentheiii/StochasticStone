using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhetStone.Looping;

namespace StochasticStone
{
    public abstract class SampleStochastic : Stochastic
    {
        public const int DEFAULT_SAMPLE_SIZE = 1000;
        public int SampleSize { get; set; }
        protected SampleStochastic(int samplesize = DEFAULT_SAMPLE_SIZE)
        {
            SampleSize = samplesize;
        }
        public override T ExpectedValue<T>(Func<double, T> func)
        {
            return generate.Generate(getValue).Take(DEFAULT_SAMPLE_SIZE).Select(func).GetAverage();
        }
        public override double ComulativeDistributionFunction(double x)
        {
            return generate.Generate(getValue).Take(DEFAULT_SAMPLE_SIZE).Count(a => a <= x) / (double)DEFAULT_SAMPLE_SIZE;
        }
    }
}
