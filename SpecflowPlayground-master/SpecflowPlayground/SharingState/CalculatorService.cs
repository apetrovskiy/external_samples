using System.Collections.Generic;
using System.Linq;

namespace SpecflowPlayground.SharingState
{
    public class CalculatorService
    {
        public int Add(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }
    }
}
