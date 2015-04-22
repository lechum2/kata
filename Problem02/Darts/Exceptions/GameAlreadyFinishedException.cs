using System;

namespace Darts.Exceptions
{
    public class GameAlreadyFinishedException : InvalidOperationException
    {
        public GameAlreadyFinishedException()
            : base("Game has finished. Create a new game.")
        {
        }
    }
}
