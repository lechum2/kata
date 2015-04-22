using System;

namespace Darts.Exceptions
{
    public class MultiplierOutOfRangeException : ArgumentOutOfRangeException
    {
        public MultiplierOutOfRangeException()
            : base("Allowed multipliers for number 25 are 1 and 2, for other numbers 1, 2, and 3.")
        {
        }
    }
}
