using System.Collections.Generic;

namespace CC_Lib.Algorithms
{
    class Temp
    {
        public IEnumerable<double> Values(double value, IEnumerable<double> manipulators, double differenceMultiplier)
        {
            
            foreach (var next in manipulators)
            {
                value += (next - value) * differenceMultiplier;
                yield return value;
            }
        }
    }
}
