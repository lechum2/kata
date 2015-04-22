using System;
using System.Collections.Generic;

namespace Darts.Exceptions
{
    public class ForbiddenNumberOfPlayersException : ArgumentOutOfRangeException
    {
        public ForbiddenNumberOfPlayersException(
            IEnumerable<int> allowedNumberOfPlayers)
            : base(
                String.Format(
                        "Allowed number of players is {0}.",
                        String.Join(", ", allowedNumberOfPlayers)))
        {
        }
    }
}
