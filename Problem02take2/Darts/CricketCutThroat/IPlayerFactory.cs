using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darts.CricketCutThroat
{
    public interface IPlayerFactory
    {
        IEnumerable<IPlayer> CreatePlayers(int playersNumber);
    }
}
