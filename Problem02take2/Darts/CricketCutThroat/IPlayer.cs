
namespace Darts.CricketCutThroat
{
    public interface IPlayer
    {
        IPlayer Threw(int number);

        int TimesPlayerThrown(int number);

        void AddPoints(int number);

        int Points { get; }
    }
}