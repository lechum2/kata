using System;

namespace Darts.CricketCutThroat.Exceptions
{
    public class InvalidPlayersNumberException : ArgumentOutOfRangeException
    {
        public InvalidPlayersNumberException()
            : base("playersNumber", "Number of players must be at least 2.")
        {
        }
    }
}
