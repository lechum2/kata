using System;

namespace Darts.Exceptions
{
    public class NumberHitOutOfRangeException : ArgumentOutOfRangeException
    {
        public NumberHitOutOfRangeException()
            : base("Numbers allowed to hit are form 1 to 20 and 25.")
        {
        }
    }
}
