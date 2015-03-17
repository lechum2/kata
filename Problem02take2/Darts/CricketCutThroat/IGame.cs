
namespace Darts.CricketCutThroat
{
    public interface IGame
    {
        void InitiateGame(int playersNumber);

        void Threw(int number);

        int[] GetPoints();
    }
}
