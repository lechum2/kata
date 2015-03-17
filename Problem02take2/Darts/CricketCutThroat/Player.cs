using System.Collections.Generic;

namespace Darts.CricketCutThroat
{
    public class Player : IPlayer
    {
        private int points;
        private IDictionary<int, int> thrownNumbers;

        public Player()
        {
            thrownNumbers = new Dictionary<int, int>();
        }

        public IPlayer Threw(int number)
        {
            if (thrownNumbers.ContainsKey(number))
            {
                thrownNumbers[number]++;
            }
            else
            {
                thrownNumbers.Add(number, 1);
            }

            return this;
        }

        public int TimesPlayerThrown(int number)
        {
            if (thrownNumbers.ContainsKey(number))
            {
                return thrownNumbers[number];
            }
            else
            {
                return 0;
            }
        }

        public void AddPoints(int number)
        {
            points += number;
        }

        public int Points
        {
            get
            {
                return points;
            }
        }
    }
}