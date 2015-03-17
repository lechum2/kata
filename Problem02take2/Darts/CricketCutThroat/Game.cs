using Darts.CricketCutThroat.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Darts.CricketCutThroat
{
    public class Game : IGame
    {
        private readonly int throwTimesRequiredToClose = 3;

        private readonly IPlayerFactory playerFactory;
        private IEnumerable<IPlayer> players;
        private IPlayer currentPlayer;

        public Game(IPlayerFactory playerFactory)
        {
            this.playerFactory = playerFactory;
        }

        public void InitiateGame(int playersNumber)
        {
            if (playersNumber < 2)
            {
                throw new InvalidPlayersNumberException();
            }

            players = playerFactory.CreatePlayers(playersNumber);
            currentPlayer = players.First();
        }

        public void Threw(int number)
        {
            if (HasPlayerClosed(currentPlayer, number))
            {
                AddPointsToPlayersThatNotClosed(number);
            }

            currentPlayer.Threw(number);
        }

        private void AddPointsToPlayersThatNotClosed(int number)
        {
            foreach(var player in players)
            {
                if (!HasPlayerClosed(player, number))
                {
                    player.AddPoints(number);
                }
            }
        }

        private bool HasPlayerClosed(IPlayer player, int number)
        {
            return player.TimesPlayerThrown(number) >= throwTimesRequiredToClose;
        }

        public int[] GetPoints()
        {
            return players.Select(player => player.Points).ToArray();
        }


    }
}
