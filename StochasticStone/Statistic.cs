using System;
using System.Linq;
using NumberStone;
using WhetStone.Fielding;
using WhetStone.Looping;
using WhetStone.Random;
using WhetStone.SystemExtensions;

namespace StochasticStone
{
    public class Statistic : Stochastic
    {
        private readonly KahanSum _sum = new KahanSum();
        private readonly KahanSum _squaresum = new KahanSum();
        private readonly MultiCollection<double> _values = new MultiCollection<double>();
        public void Add(double item)
        {
            _values.Add(item);
            _sum.Add(item);
            _squaresum.Add(item*item);
        }
        public override double getValue(RandomGenerator gen)
        {
            return _values[gen.Int(_values.Count)];
        }
        public override T ExpectedValue<T>(Func<double, T> func)
        {
            return _values.Select(func).GetSum().ToFieldWrapper()/_values.Count;
        }
        public override double ExpectedValue()
        {
            return _sum.Sum / _values.Count;
        }
        public override double ComulativeDistributionFunction(double x)
        {
            return _values.Count(a => a <= x)/(double)_values.Count;
        }
        public override double Variance()
        {
            return _squaresum.Sum/_values.Count - ExpectedValue().pow(2);
        }
    }
}
