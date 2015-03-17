using System.Collections.Generic;

namespace Darts
{
    public class Player
    {
        private IDictionary<int, int> thrownNumbers;

        public Player()
        {
            thrownNumbers = new Dictionary<int, int>();
            Points = 0;
        }

        public int Points { get; private set; }

        public int TimesThrown(int number)
        {
            if (thrownNumbers.ContainsKey(number))
            {
                return thrownNumbers[number];
            }

            return 0;
        }

        public void AddPoints(int number)
        {
            Points += number;
        }

        public void Thrown(int numberHit)
        {
            if(thrownNumbers.ContainsKey(numberHit))
            {
                thrownNumbers[numberHit]++;
            }
            else
            {
                thrownNumbers.Add(numberHit, 1);
            }
        }
    }
}
