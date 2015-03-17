using Darts.CricketCutThroat;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Darts.Tests.CricketCutThroat
{
    public class TestPlayerFactory : IPlayerFactory
    {
        private IList<IPlayer> players;

        public TestPlayerFactory()
        {
            players = new List<IPlayer>();
        }

        public IEnumerable<IPlayer> CreatePlayers(int playersNumber)
        {
            // Fill the list with empty players;
            for (int i = players.Count(); i < playersNumber; i++)
            {
                players.Add(new Player());
            }

            return players;
        }

        public IPlayer CratePlayerWithClosed(int timesRequiredToClose, params int[] numbers)
        {
            var playerMock = new  Mock<IPlayer>();

            foreach (var number in numbers)
            {
                playerMock.Setup(player => player.TimesPlayerThrown(number)).Returns(timesRequiredToClose);
            }

            return playerMock.Object;
        }

        public TestPlayerFactory IncludePlayerWithClosed(int timesRequiredToClose, params int[] numbers)
        {
            var player = CratePlayerWithClosed(timesRequiredToClose ,numbers);
            players.Add(player);
            return this;
        }

        public void Add(IPlayer player)
        {
            players.Add(player);
        }
    }
}
