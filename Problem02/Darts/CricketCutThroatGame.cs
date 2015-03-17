using System;
using System.Collections.Generic;
using System.Linq;

namespace Darts
{
    public class CricketCutThroatGame
    {

        private readonly int[] allowedNumberOfPlayers = new int[] { 2, 3, 4 };
        private readonly int throwsPerRound = 3;
        private readonly int gameRounds = 20;
        private readonly int throwsRequiredToClose = 3;

        private readonly int[] significantNumbers = new int[] { 15, 16, 17, 18, 19, 20, 25 };

        private int currentPlayerThrow;
        private int currentRound;

        private Player winner;
        private IEnumerable<Player> players;
        private IEnumerator<Player> playersEnumerator;

        public CricketCutThroatGame(int playersNumber)
        {
            if (!allowedNumberOfPlayers.Contains(playersNumber))
            {
                throw new ArgumentOutOfRangeException(
                    "playersNumber",
                    String.Format(
                        "Allowed number of players is {0}.",
                        String.Join(", ", allowedNumberOfPlayers)));
            }

            List<Player> players = new List<Player>();
            for (int i = 0; i < playersNumber; i++ )
            {
                players.Add(new Player());
            }
            this.players = players;

            this.playersEnumerator = players.GetEnumerator();
            this.playersEnumerator.MoveNext();

            currentRound = 1;
            currentPlayerThrow = 0;
            IsGameFinished = false;
        }

        public int[] Scores
        {
            get
            {
                return players.Select(p => p.Points).ToArray();
            }
        }

        public Player Winner
        {
            get
            {
                return IsGameFinished 
                    ? this.winner
                    : null;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return this.playersEnumerator.Current;
            }
        }

        public bool IsGameFinished
        {
            get;
            private set;
        }

        public CricketCutThroatGame ThrowDart(int numberHit)
        {
            return ThrowDart(numberHit, multiplier: 1);
        }

        public CricketCutThroatGame ThrowDart(int numberHit, int multiplier)
        {
            CheckIfGameIsActive();
            AssertParameters(numberHit, multiplier);
            CalculatePoints(numberHit, multiplier);
            UpdateThrowCounters();
            CheckEndGameConditions();
            return this;
        }

        private void CheckIfGameIsActive()
        {
            if (IsGameFinished)
            {
                throw new InvalidOperationException("Game has finished. Create a new game.");
            }
        }

        private void CalculatePoints(int numberHit, int multiplier)
        {
            for (int m = 0; m < multiplier; m++)
            {
                if (HasPlayerClosed(numberHit, CurrentPlayer))
                {
                    AddPointsToOtherPlayers(numberHit);
                }

                CurrentPlayer.Thrown(numberHit);
            }
        }

        private void UpdateThrowCounters()
        {
            currentPlayerThrow++;

            if (currentPlayerThrow == throwsPerRound)
            {
                if (!playersEnumerator.MoveNext())
                {
                    currentRound++;
                    playersEnumerator.Reset();
                    playersEnumerator.MoveNext();
                }
                currentPlayerThrow = 0;
            }
        }

        private void CheckEndGameConditions()
        {
            if (currentRound == gameRounds + 1)
            {
                winner = players.First();
                foreach(var player in players)
                {
                    if (player.Points > winner.Points)
                    {
                        winner = player;
                    }
                }
                IsGameFinished = true;
                return;
            }

            foreach(var player in players)
            {
                if (ClosedAll(player))
                {
                    if (!players.Any(p => p.Points < player.Points))
                    {
                        winner = player;
                        IsGameFinished = true;
                        return;
                    }
                }
            }
        }

        private bool ClosedAll(Player player)
        {
            foreach(var number in significantNumbers)
            {
                if (!HasPlayerClosed(number, player))
                {
                    return false;
                }
            }

            return true;
        }

        private void AssertParameters(int numberHit, int multiplier)
        {
            if (!IsOnTarget(numberHit))
            {
                throw new ArgumentOutOfRangeException(
                    "numberHit",
                    "Numbers allowed to hit are form 1 to 20 and 25.");
            }

            if (!IsMultiplierAllowed(numberHit, multiplier))
            {
                throw new ArgumentOutOfRangeException(
                    "multiplier",
                    "Allowed multipliers for number 25 are 1 and 2, for other numbers 1, 2, and 3.");
            }
        }

        private bool IsMultiplierAllowed(int numberHit, int multiplier)
        {
            if (numberHit == 25)
            {
                if (multiplier == 1 || multiplier == 2)
                {
                    return true;
                }
            }
            else if (multiplier == 1 || multiplier == 2 || multiplier == 3)
            {
                return true;
            }

            return false;
        }

        private void AddPointsToOtherPlayers(int numberHit)
        {
            foreach (var player in this.players)
            {
                if (!HasPlayerClosed(numberHit, player))
                {
                    player.AddPoints(numberHit);
                }
            }
        }

        private bool HasPlayerClosed(int number, Player player)
        {
            return player.TimesThrown(number) >= throwsRequiredToClose;
        }

        private bool IsOnTarget(int number)
        {
            if (number == 25) return true;
            if (number < 1) return false;
            if (number > 20) return false;

            return true;
        }
    }
}
