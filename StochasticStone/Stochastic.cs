using System;
using WhetStone.Fielding;
using WhetStone.Random;
using WhetStone.SystemExtensions;

namespace StochasticStone
{
    public abstract class Stochastic
    {
        
        public double getValue()
        {
            return getValue(new GlobalRandomGenerator());
        }
        public abstract double getValue(RandomGenerator gen);
        public abstract T ExpectedValue<T>(Func<double, T> func);
        public virtual double ExpectedValue()
        {
            return ExpectedValue(a => a);
        }
        public abstract double ComulativeDistributionFunction(double x);
        public virtual T Variance<T>(Func<double, T> func)
        {
            return ExpectedValue(a => func(a).ToFieldWrapper().pow(2)) - ExpectedValue(func).ToFieldWrapper().pow(2);
        }
        public virtual double Variance()
        {
            return ExpectedValue(a => a*a) - ExpectedValue().pow(2);
        }
    }
}
